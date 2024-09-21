using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks
{
    public class VolunteerUpdateSocialNetworksCommandValidator : AbstractValidator<VolunteerUpdateSocialNetworksCommand>
    {
        public VolunteerUpdateSocialNetworksCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleForEach(c => c.SocialNetworks).MustBeValueObject(v => SocialNetwork.Create(v.Name, v.Path));
        }
    }
}
