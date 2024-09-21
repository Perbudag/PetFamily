using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Dto;

namespace PetFamily.API.Contracts.Volunteer
{
    public record CreateVolunteerRequest(
            string Firstname, 
            string Lastname, 
            string Patronymic,
            string Description,
            int WorkExperience,
            string Email,
            string PhoneNumber,
            IEnumerable<SocialNetworkDto>? SocialNetworks,
            IEnumerable<RequisiteForAssistanceDto>? Requisites)
    {
        public CreateVolunteerCommand ToCommand()
        {
            var fullname = new FulNameDto(Firstname, Lastname, Patronymic);

            return new CreateVolunteerCommand(
                fullname,
                Description,
                WorkExperience,
                Email,
                PhoneNumber,
                SocialNetworks,
                Requisites);
        }
    }
}
