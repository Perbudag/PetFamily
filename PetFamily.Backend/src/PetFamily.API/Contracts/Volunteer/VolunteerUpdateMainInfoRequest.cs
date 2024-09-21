using PetFamily.Application.Commands.Volunteer.Update;
using PetFamily.Application.Dto;

namespace PetFamily.API.Contracts.Volunteer
{
    public record VolunteerUpdateMainInfoRequest(
        FulNameDto Fullname,
        string Description,
        int WorkExperience,
        string Email,
        string PhoneNumber)
    {
        public VolunteerUpdateMainInfoCommand ToCommand(Guid id) =>
            new VolunteerUpdateMainInfoCommand(id, Fullname, Description, WorkExperience, Email, PhoneNumber);
    }
}
