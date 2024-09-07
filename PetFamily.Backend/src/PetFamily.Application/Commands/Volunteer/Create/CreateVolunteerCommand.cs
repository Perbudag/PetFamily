using PetFamily.Application.Dto;

namespace PetFamily.Application.Commands.Volunteer.Create
{
    public record CreateVolunteerCommand(
            string Firstname,
            string Lastname,
            string Patronymic,

            string Description,

            int WorkExperience,
            int PetsFoundHomeCount,
            int PetsLookingForHomeCount,
            int PetsOnTreatmentCount,

            string Email,

            string PhoneNumber,

            IEnumerable<SocialNetworkDto>? SocialNetworks,
            IEnumerable<RequisiteForAssistanceDto>? Requisites
        );
}
