using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.Update
{
    public interface IUpdateVolunteerMainInfoHandler
    {
        public Task<Result<Guid>> Handle(UpdateVolunteerMainInfoCommand command, CancellationToken cancellationToken = default);
    }
}
