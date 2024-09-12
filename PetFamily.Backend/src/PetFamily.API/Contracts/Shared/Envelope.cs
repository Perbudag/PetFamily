using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Contracts.Shared
{
    public record Envelope
    {
        public object? Result { get; }
        public string? ErrorCode { get; }
        public string? ErrorMessage { get; }
        public DateTime TimeGenerated { get; }

        public Envelope(object? result, Error? error)
        {
            Result = result;
            ErrorCode = error?.Code;
            ErrorMessage = error?.Message;
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok(object? result) =>
            new(result, null);

        public static Envelope Error(Error error) =>
            new(null, error);
    }
}