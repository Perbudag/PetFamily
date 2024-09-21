using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.UpdateRequisites
{
    public interface IUpdateRequisitesHandler
    {
        public Task<Result<Guid>> Handle(UpdateRequisitesCommand command, CancellationToken cancellationToken = default);
    }
}
