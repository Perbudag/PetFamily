using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Contracts.Shared;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult<T> ToResponse<T>(this Result<T> result, int successStatusCode = StatusCodes.Status200OK)
        {
            if(result.IsSuccess)
                return new ObjectResult(Envelope.Ok(result.Value))
                {
                    StatusCode = successStatusCode
                };

            var statusCode = GetStatusCodeForErrorType(result.Errors[0].Type);

            var responseErrors = result.Errors.Select(e => new ResponseError(e.Code, e.Message, e.InvalidField));

            var envelope = Envelope.Error(responseErrors);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult ToResponse(this Result result, int successStatusCode = StatusCodes.Status200OK)
        {
            if (result.IsSuccess)
                return new ObjectResult(Envelope.Ok(null))
                {
                    StatusCode = successStatusCode
                };

            var statusCode = GetStatusCodeForErrorType(result.Errors[0].Type);

            var responseErrors = result.Errors.Select(e => new ResponseError(e.Code, e.Message, e.InvalidField));

            var envelope = Envelope.Error(responseErrors);

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