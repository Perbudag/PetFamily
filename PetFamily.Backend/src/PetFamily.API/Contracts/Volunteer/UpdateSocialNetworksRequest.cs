using PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks;
using PetFamily.Application.Dto;

namespace PetFamily.API.Contracts.Volunteer
{
    public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid id) =>
            new UpdateSocialNetworksCommand(id, SocialNetworks);
    }
}
