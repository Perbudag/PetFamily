using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.Update
{
    public interface IVolunteerUpdateMainInfoHandler
    {
        public Task<Result<Guid>> Handle(VolunteerUpdateMainInfoCommand command, CancellationToken cancellationToken = default);
    }
}
