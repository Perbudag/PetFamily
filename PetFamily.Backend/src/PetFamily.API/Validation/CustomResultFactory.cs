using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using PetFamily.API.Contracts.Shared;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Validation
{
    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
        {
            if (validationProblemDetails is null)
                throw new InvalidOperationException("ValidationProblemDetails is null.");

            List<ResponseError> errors = [];

            foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
            {
                var responseError = from errorMessage in validationErrors
                                    let errorDetails = Error.Deserialize(errorMessage)
                                    select new ResponseError(errorDetails.Code, errorDetails.Message, invalidField);

                errors.AddRange(responseError);
            }

            var envelope = Envelope.Error(errors);

            return new ObjectResult(envelope)
            {
                StatusCode = validationProblemDetails.Status
            };
        }
    }
}
