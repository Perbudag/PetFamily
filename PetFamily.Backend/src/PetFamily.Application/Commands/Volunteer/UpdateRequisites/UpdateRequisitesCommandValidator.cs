using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Commands.Volunteer.UpdateRequisites
{
    public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
    {
        public UpdateRequisitesCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleForEach(c => c.Requisites).MustBeValueObject(v => Requisite.Create(v.Title, v.Description));
        }
    }
}
