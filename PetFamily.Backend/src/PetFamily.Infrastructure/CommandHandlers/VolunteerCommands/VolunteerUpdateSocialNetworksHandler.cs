using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.API.Extensions;
using PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Infrastructure.Database;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class VolunteerUpdateSocialNetworksHandler : IVolunteerUpdateSocialNetworksHandler
    {
        private readonly PetFamilyDbContext _context;
        private readonly ILogger<VolunteerUpdateSocialNetworksHandler> _logger;
        private readonly IValidator<VolunteerUpdateSocialNetworksCommand> _validator;

        public VolunteerUpdateSocialNetworksHandler(PetFamilyDbContext context, ILogger<VolunteerUpdateSocialNetworksHandler> logger, IValidator<VolunteerUpdateSocialNetworksCommand> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(VolunteerUpdateSocialNetworksCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(command);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var volunteerId = VolunteerId.Create(command.Id);

            var socialNetworks = new List<SocialNetwork>();

            if (command.SocialNetworks != null)
            {
                socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Path).Value)
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

            volunteer!.UpdateSocialNetworks(socialNetworks);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Updating the social networks of volunteers with id: {id}",
                command.Id);

            return volunteer.Id.Value;
        }
    }
}
