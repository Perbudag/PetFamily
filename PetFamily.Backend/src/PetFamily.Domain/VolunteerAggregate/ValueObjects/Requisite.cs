using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record Requisite
    {
        public const int TITLE_MAX_LENGTH = 250;
        public const int DESCRIPTION_MAX_LENGTH = 500;


        public string Title { get; }
        public string Description { get; }


        private Requisite(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public static Result<Requisite> Create(string title, string description)
        {
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(title) || title.Length > TITLE_MAX_LENGTH)
                errors.Add(Errors.General.Validation.String.NotBeEmptyAndNotBeLonger("requisite.title", TITLE_MAX_LENGTH));
            
            if(string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                errors.Add(Errors.General.Validation.String.NotBeEmptyAndNotBeLonger("requisite.description", DESCRIPTION_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;

            var requisite = new Requisite(title, description);

            return requisite;
        }
    }
}