using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Commands.Volunteer.Create
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator()
        {
            RuleFor(c => c.Fullname)
                .MustBeValueObject(v => FullName.Create(v.Firstname, v.Lastname, v.Patronymic));

            RuleFor(c => c.Description).MustBeValueObject(Description.Create);

            RuleFor(c => c.WorkExperience).MustBeValueObject(WorkExperience.Create);

            RuleFor(c => c.Email).MustBeValueObject(EmailAddress.Parse);

            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

            RuleForEach(c => c.SocialNetworks).MustBeValueObject(v => SocialNetwork.Create(v.Name, v.Path));

            RuleForEach(c => c.Requisites).MustBeValueObject(v => Requisite.Create(v.Title, v.Description));
        }
    }
}
