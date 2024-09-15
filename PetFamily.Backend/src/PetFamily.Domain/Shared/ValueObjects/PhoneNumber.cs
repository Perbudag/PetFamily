using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public record PhoneNumber
    {
        public const int PHONE_NUMBER_LENGTH = 11;

        public string Value { get; }

        private PhoneNumber(string phoneNumber)
        {
            Value = phoneNumber;
        }

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            List<Error> errors = [];

            try
            {
                var x = long.Parse(phoneNumber);

                phoneNumber = x.ToString();

                if (phoneNumber.Length != PHONE_NUMBER_LENGTH)
                    errors.Add(Error.Validation("phoneNumber" + Errors.Validation.ErrorCode, $"The \"phoneNumber\" field must contain {PHONE_NUMBER_LENGTH} digits."));

            }
            catch
            {
                errors.Add(Error.Validation("phoneNumber" + Errors.Validation.ErrorCode, $"The \"phoneNumber\" field must contain {PHONE_NUMBER_LENGTH} digits."));
            }


            if (errors.Count > 0)
                return errors;

            return new PhoneNumber(phoneNumber);
        }
    }
}
