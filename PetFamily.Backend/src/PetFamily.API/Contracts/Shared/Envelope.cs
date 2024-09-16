using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Contracts.Shared
{
    public record ResponseError(string Code, string Message, string? InvalidField);

    public record Envelope
    {
        public object? Result { get; }
        public IEnumerable<ResponseError> Errors { get; }
        public DateTime TimeGenerated { get; }

        public Envelope(object? result, IEnumerable<ResponseError> errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok(object? result) =>
            new(result, []);

        public static Envelope Error(IEnumerable<ResponseError> errors) =>
            new(null, errors);
    }
}