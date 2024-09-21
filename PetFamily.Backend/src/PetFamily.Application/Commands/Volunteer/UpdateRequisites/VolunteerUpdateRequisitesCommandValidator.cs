using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Commands.Volunteer.UpdateRequisites
{
    public class VolunteerUpdateRequisitesCommandValidator : AbstractValidator<VolunteerUpdateRequisitesCommand>
    {
        public VolunteerUpdateRequisitesCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleForEach(c => c.Requisites).MustBeValueObject(v => Requisite.Create(v.Title, v.Description));
        }
    }
}
