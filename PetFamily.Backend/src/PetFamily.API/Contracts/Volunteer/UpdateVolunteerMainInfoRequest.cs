using PetFamily.Application.Commands.Volunteer.Update;
using PetFamily.Application.Dto;

namespace PetFamily.API.Contracts.Volunteer
{
    public record UpdateVolunteerMainInfoRequest(
        FulNameDto Fullname,
        string Description,
        int WorkExperience,
        string Email,
        string PhoneNumber)
    {
        public UpdateVolunteerMainInfoCommand ToCommand(Guid id) =>
            new UpdateVolunteerMainInfoCommand(id, Fullname, Description, WorkExperience, Email, PhoneNumber);
    }
}
