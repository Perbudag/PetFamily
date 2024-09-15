using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record EmailAddress
    {
        public const int EMAIL_MAX_LENGTH = 320;
        public const int EMAIL_USER_NAME_LENGTH = 64;
        public const int EMAIL_DOMAIN_NAME_LENGTH = 255;

        public string UserName { get; }
        public string DomainName { get; }

        private EmailAddress(string userName, string domainName)
        {
            UserName = userName;
            DomainName = domainName;
        }

        public override string ToString()=>
            $"{UserName}@{DomainName}";
        
        public static Result<EmailAddress> Parse(string email)
        {
            List<Error> errors = [];

            if(string.IsNullOrWhiteSpace(email) || email.Length > EMAIL_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("email", EMAIL_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;


            var strs = email.Split('@');


            if (strs.Length != 2)
                errors.Add(Error.Validation(
                    "Email" + Errors.Validation.ErrorCode,
                    "The email address must consist of 2 lines separated by the '@' character"));

            if (errors.Count > 0)
                return errors;


            return Create(strs[0], strs[1]);
        }

        public static Result<EmailAddress> Create(string userName, string domainName)
        {
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(userName) || userName.Length > EMAIL_USER_NAME_LENGTH)
                errors.Add(Error.Validation(
                    "Email" + Errors.Validation.ErrorCode,
                    $"The line before the '@' character of the email address should not be empty and should not be longer than {EMAIL_USER_NAME_LENGTH} characters"));

            if (string.IsNullOrWhiteSpace(domainName) || domainName.Length > EMAIL_DOMAIN_NAME_LENGTH)
                errors.Add(Error.Validation(
                    "Email" + Errors.Validation.ErrorCode,
                    $"The line after the '@' character in the email address should not be empty and contain no more than {EMAIL_DOMAIN_NAME_LENGTH} characters"));

            if (errors.Count > 0)
                return errors;

            var address = new EmailAddress(userName, domainName);

            return address;
        }
    }
}
