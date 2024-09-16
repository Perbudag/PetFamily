using System.Linq;

namespace PetFamily.Domain.Shared.Models
{
    public static class Errors
    {
        public static class General
        {

            public static class Validation
            {

                public const string ErrorCode = ".Is.Invalid";

                public static Error IsInvalid(string fieldName) =>
                    Error.Validation(
                        FormatErrorCode(fieldName, ErrorCode),
                        $"The {fieldName} field is invalid");


                public static Error NotBeEmpty(string fieldName) =>
                    Error.Validation(
                        FormatErrorCode(fieldName, ErrorCode),
                        $"The \"{fieldName}\" field should not be empty.");



                public static class Int
                {
                    public static Error MustBeGreaterThanZero(string fieldName) =>
                       Error.Validation(
                           FormatErrorCode(fieldName, ErrorCode),
                           $"The \"{fieldName}\" field must be greater than zero.");

                    public static Error MustBePositive(string fieldName) =>
                       Error.Validation(
                           FormatErrorCode(fieldName, ErrorCode),
                           $"The field \"{fieldName}\" must be a positive number.");
                }



                public static class String
                {
                    public static Error NotBeEmptyAndNotBeLonger(string fieldName, int maxLength) =>
                        Error.Validation(
                            FormatErrorCode(fieldName, ErrorCode),
                            $"The \"{fieldName}\" field should not be empty and should contain no more than {maxLength} characters.");

                    public static Error NotBeLonger(string fieldName, int maxLength) =>
                        Error.Validation(
                                FormatErrorCode(fieldName, ErrorCode),
                            $"The \"{fieldName}\" field must not contain more than {maxLength} characters");


                    public static Error EqualToLength(string fieldName, int length) =>
                        Error.Validation(
                            FormatErrorCode(fieldName, ErrorCode),
                            $"The \"{fieldName}\" field must contain {length} characters.");

                }
            }


            public static class Conflict
            {
                public const string ErrorCode = ".Exists";

                public static Error IsExists(string EntityName, string propertyName) =>
                    Error.Conflict(
                        FormatErrorCode($"{EntityName}.{propertyName}", ErrorCode),
                        $"A {EntityName} with this {propertyName} already exists.");
            }

        }

        private static string FormatErrorCode(string source, string errorCode)
        {
            string[] sourceLines = source.Split('.');
            string result = "";

            foreach (var line in sourceLines)
            {
                var lineParts = line.Split(' ');

                for (int i = 0; i < lineParts.Length; i++)
                {
                    lineParts[i] = lineParts[i].Substring(0, 1).ToUpper() + lineParts[i].Substring(1);
                }

                result += string.Join("", lineParts) + '.';
            }

            result += errorCode;

            result = result.Replace("..", ".");

            return result;
        }
    }
}
