
using PetFamily.Application.Dto;

namespace PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks
{
    public record UpdateSocialNetworksCommand(Guid Id, IEnumerable<SocialNetworkDto> SocialNetworks);
}
