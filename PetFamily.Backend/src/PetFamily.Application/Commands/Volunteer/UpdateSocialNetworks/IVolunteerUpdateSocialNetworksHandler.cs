using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks
{
    public interface IVolunteerUpdateSocialNetworksHandler
    {
        public Task<Result<Guid>> Handle(VolunteerUpdateSocialNetworksCommand command, CancellationToken cancellationToken = default);
    }
}
