using CSharpFunctionalExtensions;

namespace PetFamily.Application.Commands.Volunteer.Create
{
    public interface ICreateVolunteerHandler
    {
        public Task<Result<Guid>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken = default);
    }
}
