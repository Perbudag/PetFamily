using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.Create
{
    public interface IVolunteerCreateHandler
    {
        public Task<Result<Guid>> Handle(VolunteerCreateCommand command, CancellationToken cancellationToken = default);
    }
}
