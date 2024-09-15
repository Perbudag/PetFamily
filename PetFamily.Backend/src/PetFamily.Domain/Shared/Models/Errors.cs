namespace PetFamily.Domain.Shared.Models
{
    public static class Errors
    {
        public static class Validation {

            public const string ErrorCode = ".Is.Invalid";

            public static Error IsInvalid(string fieldName) =>
                Error.Validation(
                    ToUpperFirstLetter(fieldName) + ErrorCode,
                    $"The {fieldName} field is invalid");


            public static Error NotBeEmpty(string fieldName) =>
                Error.Validation(
                    ToUpperFirstLetter(fieldName) + ErrorCode,
                    $"The \"{fieldName}\" field should not be empty.");



            public static class Int
            {
                public static Error MustBeGreaterThanZero(string fieldName) =>
                   Error.Validation(
                       ToUpperFirstLetter(fieldName) + ErrorCode,
                       $"The \"{fieldName}\" field must be greater than zero.");

                public static Error MustBePositive(string fieldName) =>
                   Error.Validation(
                       ToUpperFirstLetter(fieldName) + ErrorCode,
                       $"The field \"{fieldName}\" must be a positive number.");
            }



            public static class String
            {
                public static Error NotBeEmptyAndNotBeLonger(string fieldName, int maxLength) =>
                    Error.Validation(
                        ToUpperFirstLetter(fieldName) + ErrorCode,
                        $"The \"{fieldName}\" field should not be empty and should contain no more than {maxLength} characters.");

                public static Error NotBeLonger(string fieldName, int maxLength) =>
                    Error.Validation(
                        ToUpperFirstLetter(fieldName) + ErrorCode,
                        $"The \"{fieldName}\" field must not contain more than {maxLength} characters");


                public static Error EqualToLength(string fieldName, int length) =>
                    Error.Validation(
                        ToUpperFirstLetter(fieldName) + ErrorCode,
                        $"The \"{fieldName}\" field must contain {length} characters.");

            }
        }

        private static string ToUpperFirstLetter(string source)
        {
            string[] sourceLines = source.Split('.');
            string result = "";


            foreach (var line in sourceLines)
            {
                result += line.Substring(0, 1).ToUpper() + line.Substring(1) + '.';
            }

            result = result.Remove(result.Length - 1);

            return result;
        }
    }
}
