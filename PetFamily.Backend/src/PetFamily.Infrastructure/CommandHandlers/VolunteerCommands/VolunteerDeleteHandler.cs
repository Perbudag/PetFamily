using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.API.Extensions;
using PetFamily.Application.Commands.Volunteer.Delete;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;
using PetFamily.Infrastructure.Database;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class VolunteerDeleteHandler : IVolunteerDeleteHandler
    {
        private readonly PetFamilyDbContext _context;
        private readonly ILogger<VolunteerDeleteHandler> _logger;
        private readonly IValidator<VolunteerDeleteCommand> _validator;

        public VolunteerDeleteHandler(PetFamilyDbContext context, ILogger<VolunteerDeleteHandler> logger, IValidator<VolunteerDeleteCommand> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(VolunteerDeleteCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(command);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var volunteerId = VolunteerId.Create(command.Id);

            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

            List<Error> errors = [];

            if (volunteer is null)
            {
                errors.Add(Errors.Volunteer.NotFound("ID"));
            }

            if (errors.Count > 0)
                return errors;

            volunteer!.Delete();

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Delete volunteer with id: {id}",
                command.Id);

            return volunteerId.Value;
        }
    }
}
