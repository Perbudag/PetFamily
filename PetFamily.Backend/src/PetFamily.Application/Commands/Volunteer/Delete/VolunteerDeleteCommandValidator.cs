using FluentValidation;

namespace PetFamily.Application.Commands.Volunteer.Delete
{
    public class VolunteerDeleteCommandValidator : AbstractValidator<VolunteerDeleteCommand>
    {
        public VolunteerDeleteCommandValidator()
        {
            RuleFor(d => d.Id).NotEmpty();
        }
    }
}
