﻿using PetFamily.Domain.Shared.Models;

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
            if(string.IsNullOrWhiteSpace(email) || email.Length > EMAIL_MAX_LENGTH)
                return Error.Validation("EmailAddress.Parse.Invalid", $"The \"email\" argument must not be empty and must consist of no more than {EMAIL_MAX_LENGTH} characters.");

            var strs = email.Split('@');

            if (strs.Length != 2)
                return Error.Validation("EmailAddress.Parse.Invalid", "Invalid email address");

            return Create(strs[0], strs[1]);
        }

        public static Result<EmailAddress> Create(string userName, string domainName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length > EMAIL_USER_NAME_LENGTH)
                return Error.Validation("EmailAddress.Create.Invalid", $"The \"userName\" argument must not be empty and must consist of no more than {EMAIL_USER_NAME_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(domainName) || domainName.Length > EMAIL_DOMAIN_NAME_LENGTH)
                return Error.Validation("EmailAddress.Create.Invalid", $"The \"domainName\" argument must not be empty and must consist of no more than {EMAIL_DOMAIN_NAME_LENGTH} characters.");

            var address = new EmailAddress(userName, domainName);

            return address;
        }
    }
}
