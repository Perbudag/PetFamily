using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.Delete
{
    public interface IVolunteerDeleteHandler
    {
        public Task<Result<Guid>> Handle(VolunteerDeleteCommand command, CancellationToken cancellationToken = default);
    }
}
