using PetFamily.Domain.Shared.Models;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public record PhoneNumber
    {
        public const int PHONE_NUMBER_LENGTH = 11;

        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            List<Error> errors = [];

            
            if (phoneNumber.Length != PHONE_NUMBER_LENGTH || !Regex.IsMatch(phoneNumber, @"^[0-9]+$"))
                errors.Add(Error.Validation("PhoneNumber" + Errors.General.Validation.ErrorCode, $"The \"phoneNumber\" field must contain {PHONE_NUMBER_LENGTH} digits."));


            if (errors.Count > 0)
                return errors;

            return new PhoneNumber(phoneNumber);
        }
    }
}
