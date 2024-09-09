using CSharpFunctionalExtensions;
using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Interfaces.Repositories;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Infrastructure.Database;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class CreateVolunteerHandler : ICreateVolunteerHandler
    {
        private readonly PetFamilyDbContext _dbContext;
        private readonly IVolunteerRepositoriy _repositoriy;

        public CreateVolunteerHandler(PetFamilyDbContext dbContext, IVolunteerRepositoriy repositoriy)
        {
            _dbContext = dbContext;
            _repositoriy = repositoriy;
        }

        public async Task<Result<Guid>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken)
        {
            var fullname = FullName.Create(command.Firstname, command.Lastname, command.Patronymic);

            if (fullname.IsFailure)
                return Result.Failure<Guid>(fullname.Error);

            var email = EmailAddress.Parse(command.Email);

            if (email.IsFailure)
                return Result.Failure<Guid>(email.Error);

            if (await _repositoriy.EmailExists(email.Value, cancellationToken))
                return Result.Failure<Guid>("A volunteer with this email address already exists.");

            if (await _repositoriy.PhoneNumberExists(command.PhoneNumber, cancellationToken))
                return Result.Failure<Guid>("A volunteer with this phone number already exists.");

            var socialNetworks = new List<Result<SocialNetwork>>();

            if(command.SocialNetworks != null)
            {
                socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Path))
                .ToList();

                foreach (var item in socialNetworks)
                {
                    if (item.IsFailure)
                        return Result.Failure<Guid>(item.Error);
                }
            }
            
            var requisites =  new List<Result<RequisiteForAssistance>>();

            if (command.Requisites != null)
            {
                requisites = command.Requisites
                .Select(x => RequisiteForAssistance.Create(x.Title, x.Description))
                .ToList();

                foreach (var item in requisites)
                {
                    if (item.IsFailure)
                        return Result.Failure<Guid>(item.Error);
                }
            }
            
            var volunteer = Volunteer.Create(
                fullname.Value,
                command.Description,
                command.WorkExperience,
                email.Value,
                command.PhoneNumber,
                socialNetworks.Select(x => x.Value).ToList(),
                requisites.Select(x => x.Value).ToList(),
                new List<Pet>());

            if (volunteer.IsFailure)
                return Result.Failure<Guid>(volunteer.Error);

            await _dbContext.Volunteers.AddAsync(volunteer.Value, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
                
            return volunteer.Value.Id.Value;
        }
    }
}
