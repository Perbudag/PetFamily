using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Contracts.Shared
{
    public record Envelope
    {
        public object? Result { get; }
        public IEnumerable<ErrorEnvelope> Errors { get; }
        public DateTime TimeGenerated { get; }

        public Envelope(object? result, IEnumerable<Error> errors)
        {
            Result = result;
            Errors = errors.Select(e => new ErrorEnvelope(e.Code, e.Message));
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok(object? result) =>
            new(result, []);

        public static Envelope Error(List<Error> error) =>
            new(null, error);
    }

    public record ErrorEnvelope(string Code, string Message);
}