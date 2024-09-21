using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.API.Extensions;
using PetFamily.Application.Commands.Volunteer.UpdateRequisites;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;
using PetFamily.Infrastructure.Database;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class UpdateRequisitesHandler : IUpdateRequisitesHandler
    {
        private readonly PetFamilyDbContext _context;
        private readonly ILogger<UpdateRequisitesHandler> _logger;
        private readonly IValidator<UpdateRequisitesCommand> _validator;

        public UpdateRequisitesHandler(PetFamilyDbContext context, ILogger<UpdateRequisitesHandler> logger, IValidator<UpdateRequisitesCommand> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(UpdateRequisitesCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(command);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var volunteerId = VolunteerId.Create(command.Id);

            var requisites = new List<Requisite>();

            if (command.Requisites != null)
            {
                requisites = command.Requisites
                .Select(x => Requisite.Create(x.Title, x.Description).Value)
                .ToList();
            }

            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);


            List<Error> errors = [];

            if (volunteer is null)
            {
                errors.Add(Error.NotFound("Volunteer.Not.Found", "A volunteer with this ID was not found."));
            }

            if (errors.Count > 0)
                return errors;

            volunteer!.UpdateRequisites(requisites);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Update volunteer requisites with id: {id}",
                command.Id);

            return volunteer.Id.Value;
        }
    }
}