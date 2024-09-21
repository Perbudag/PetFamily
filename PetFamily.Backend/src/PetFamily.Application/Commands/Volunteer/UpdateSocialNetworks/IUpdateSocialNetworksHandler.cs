using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks
{
    public interface IUpdateSocialNetworksHandler
    {
        public Task<Result<Guid>> Handle(UpdateSocialNetworksCommand command, CancellationToken cancellationToken = default);
    }
}
