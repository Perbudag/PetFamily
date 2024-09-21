using FluentValidation.Results;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Extensions
{
    public static class ValidationResultExtensions
    {
        public static List<Error> ToErrorList(this ValidationResult validationResult)
        {
            var errors = from error in validationResult.Errors
                         select Error.Validation(error.ErrorCode, error.ErrorMessage, error.PropertyName);


            var x = errors.ToList();

            return errors.ToList();
        }
    }
}
