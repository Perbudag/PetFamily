using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record FullName
    {
        public static readonly int FIRSTNAME_MAX_LENGTH = 50;
        public static readonly int LASTNAME_MAX_LENGTH = 50;
        public static readonly int PATRONYMIC_MAX_LENGTH = 50;

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
            if(string.IsNullOrWhiteSpace(firstname) || firstname.Length > FIRSTNAME_MAX_LENGTH)
                return Result.Failure<FullName>($"The \"name\" argument must not be empty and must consist of no more than {FIRSTNAME_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(lastname) || lastname.Length > LASTNAME_MAX_LENGTH)
                return Result.Failure<FullName>($"The \"lastname\" argument must not be empty and must consist of no more than {LASTNAME_MAX_LENGTH} characters.");

            if(patronymic.Length > PATRONYMIC_MAX_LENGTH)
                return Result.Failure<FullName>($"The \"patronymic\" argument must not contain more than {PATRONYMIC_MAX_LENGTH} characters");

            var fullName = new FullName(firstname, lastname, patronymic);

            return Result.Success(fullName);
        }
    }
}