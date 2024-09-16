using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Interfaces.Repositories;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Infrastructure.Database;
using System.Collections.Generic;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class CreateVolunteerHandler : ICreateVolunteerHandler
    {
        private readonly PetFamilyDbContext _dbContext;
        private readonly IVolunteerRepositoriy _repository;

        public CreateVolunteerHandler(PetFamilyDbContext dbContext, IVolunteerRepositoriy repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken)
        {
            List<Error> errors = [];

            var fullname = FullName.Create(command.Firstname, command.Lastname, command.Patronymic);

            var description = Description.Create(command.Description);

            var workExperience = WorkExperience.Create(command.WorkExperience);

            var email = EmailAddress.Parse(command.Email);

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber);

            var socialNetworks = new List<Result<SocialNetwork>>();

            if(command.SocialNetworks != null)
            {
                socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Path))
                .ToList();
            }

            var requisites =  new List<Result<Requisite>>();

            if (command.Requisites != null)
            {
                requisites = command.Requisites
                .Select(x => Requisite.Create(x.Title, x.Description))
                .ToList();
            }


            if (fullname.IsFailure)
                errors.AddRange(fullname.Errors);

            if (description.IsFailure)
                errors.AddRange(description.Errors);

            if (workExperience.IsFailure)
                errors.AddRange(workExperience.Errors);


            if (email.IsFailure)
                errors.AddRange(email.Errors);

            else if (await _repository.EmailExists(email.Value, cancellationToken))
                errors.Add(Error.Conflict("CreateVolunteer.Email.Exists", "A volunteer with this email address already exists."));


            if (phoneNumber.IsFailure)
                errors.AddRange(phoneNumber.Errors);

            else if (await _repository.PhoneNumberExists(phoneNumber.Value, cancellationToken))
                errors.Add(Error.Conflict("CreateVolunteer.Phone.Exists", "A volunteer with this phone number already exists."));


            foreach (var item in socialNetworks)
            {
                if (item.IsFailure)
                    errors.AddRange(item.Errors);
            }

            foreach (var item in requisites)
            {
                if (item.IsFailure)
                    errors.AddRange(item.Errors);
            }


            if (errors.Count > 0)
                return errors;

            var volunteer = new Volunteer(
                fullname.Value,
                description.Value,
                workExperience.Value,
                email.Value,
                phoneNumber.Value,
                socialNetworks.Select(x => x.Value).ToList(),
                requisites.Select(x => x.Value).ToList());

            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
                
            return volunteer.Id.Value;
        }
    }
}
