using PetFamily.Application.Dto;

namespace PetFamily.Application.Commands.Volunteer.Update
{
    public record VolunteerUpdateMainInfoCommand(
        Guid Id,
        FulNameDto Fullname,
        string Description,
        int WorkExperience,
        string Email,
        string PhoneNumber
        );
}
