using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Contracts.Shared;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Extensions
{
    public static class ResponseExtensions
    {
        public static ActionResult<T> ToResponse<T>(this Result<T> result)
        {
            if(result.IsSuccess)
                return new OkObjectResult(Envelope.Ok(result.Value));

            var statusCode = GetStatusCodeForErrorType(result.Errors[0].Type);

            var envelope = Envelope.Error(result.Errors);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode
            };
        }

        private static int GetStatusCodeForErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}