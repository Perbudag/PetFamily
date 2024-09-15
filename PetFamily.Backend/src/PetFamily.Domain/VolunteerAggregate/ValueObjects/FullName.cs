using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record FullName
    {
        public const int FIRSTNAME_MAX_LENGTH = 50;
        public const int LASTNAME_MAX_LENGTH = 50;
        public const int PATRONYMIC_MAX_LENGTH = 50;

        public string Firstname { get; }
        public string Lastname { get; }
        public string Patronymic { get; }


        private FullName(string firstname, string lastname, string patronymic)
        {
            Firstname = firstname;
            Lastname = lastname;
            Patronymic = patronymic;
        }

        public static Result<FullName> Create(string firstname, string lastname, string patronymic)
        {
            List<Error> errors = [];

            if(string.IsNullOrWhiteSpace(firstname) || firstname.Length > FIRSTNAME_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("firstname", FIRSTNAME_MAX_LENGTH));

            if(string.IsNullOrWhiteSpace(lastname) || lastname.Length > LASTNAME_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("lastname", LASTNAME_MAX_LENGTH));

            if(patronymic.Length > PATRONYMIC_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeLonger("patronymic", PATRONYMIC_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;

            var fullName = new FullName(firstname, lastname, patronymic);

            return fullName;
        }
    }
}