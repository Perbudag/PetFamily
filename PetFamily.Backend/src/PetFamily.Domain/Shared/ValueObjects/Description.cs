using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public record Description
    {
        public const int DESCRIPTION_MAX_LENGTH = 500;

        public string Value { get; }

        private Description(string value)
        {
            Value = value;
        }

        public static Result<Description> Create(string description)
        {
            List<Error> errors = [];

            if (description.Length > DESCRIPTION_MAX_LENGTH)
                errors.Add(Errors.General.Validation.String.NotBeLonger("description", DESCRIPTION_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;

            return new Description(description);
        }
    }
}
