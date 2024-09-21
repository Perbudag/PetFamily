using FluentValidation;
using FluentValidation.Results;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject>> factoryMethod)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject> result = factoryMethod(value);

                if (result.IsSuccess)
                    return;

                foreach (var error in result.Errors)
                {
                    var validationResult = new ValidationFailure
                    {
                        ErrorCode = error.Code,
                        ErrorMessage = error.Message,
                        PropertyName = context.PropertyName
                    };

                    context.AddFailure(validationResult);
                }
            });
        }
    }
}
