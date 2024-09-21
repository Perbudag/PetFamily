using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.UpdateRequisites
{
    public interface IVolunteerUpdateRequisitesHandler
    {
        public Task<Result<Guid>> Handle(VolunteerUpdateRequisitesCommand command, CancellationToken cancellationToken = default);
    }
}
