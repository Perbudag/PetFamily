using PetFamily.Application.Dto;

namespace PetFamily.Application.Commands.Volunteer.Create
{
    public record CreateVolunteerCommand(
            FulNameDto Fullname,
            string Description,
            int WorkExperience,
            string Email,
            string PhoneNumber,
            IEnumerable<SocialNetworkDto>? SocialNetworks,
            IEnumerable<RequisiteForAssistanceDto>? Requisites
        );
}
