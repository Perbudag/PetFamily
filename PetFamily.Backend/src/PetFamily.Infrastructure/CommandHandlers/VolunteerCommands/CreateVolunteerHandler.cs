using Microsoft.Extensions.Logging;
using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Interfaces.Repositories;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Infrastructure.Database;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class CreateVolunteerHandler : ICreateVolunteerHandler
    {
        private readonly PetFamilyDbContext _dbContext;
        private readonly IVolunteerRepositoriy _repository;
        private readonly ILogger<CreateVolunteerHandler> _logger;

        public CreateVolunteerHandler(PetFamilyDbContext dbContext, IVolunteerRepositoriy repository, ILogger<CreateVolunteerHandler> logger)
        {
            _dbContext = dbContext;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken)
        {
            var fullname = FullName.Create(
                command.Fullname.Firstname,
                command.Fullname.Lastname,
                command.Fullname.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var workExperience = WorkExperience.Create(command.WorkExperience).Value;

            var email = EmailAddress.Parse(command.Email).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var socialNetworks = new List<SocialNetwork>();

            if(command.SocialNetworks != null)
            {
                socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Path).Value)
                .ToList();
            }

            var requisites =  new List<Requisite>();

            if (command.Requisites != null)
            {
                requisites = command.Requisites
                .Select(x => Requisite.Create(x.Title, x.Description).Value)
                .ToList();
            }



            List<Error> errors = [];


            if (await _repository.EmailExists(email, cancellationToken))
                errors.Add(Errors.General.Conflict.IsExists("volunteer", "email"));

            if (await _repository.PhoneNumberExists(phoneNumber, cancellationToken))
                errors.Add(Errors.General.Conflict.IsExists("volunteer", "phone number"));


            if (errors.Count > 0)
                return errors;



            var volunteer = new Volunteer(
                fullname,
                description,
                workExperience,
                email,
                phoneNumber,
                socialNetworks,
                requisites);

            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("The volunteer was created with the ID: {volunteerId}", volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
