using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public record Name
    {
        public const int NAME_MAX_LENGTH = 50;

        public string Value { get; }

        private Name(string name)
        {
            Value = name;
        }

        public static Result<Name> Create(string name)
        {
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("name", NAME_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;

            return new Name(name);
        }
    }
}
