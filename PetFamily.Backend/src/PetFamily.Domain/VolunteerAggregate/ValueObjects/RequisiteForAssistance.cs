using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record RequisiteForAssistance
    {
        public const int TITLE_MAX_LENGTH = 250;
        public const int DESCRIPTION_MAX_LENGTH = 500;


        public string Title { get; }
        public string Description { get; }


        private RequisiteForAssistance(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public static Result<RequisiteForAssistance> Create(string title, string description)
        {
            if(string.IsNullOrWhiteSpace(title) || title.Length > TITLE_MAX_LENGTH)
                return Error.Validation("RequisiteForAssistance.Create.Invalid", $"The \"title\" argument must not be empty and must consist of no more than {TITLE_MAX_LENGTH} characters.");
            
            if(string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                return Error.Validation("RequisiteForAssistance.Create.Invalid", $"The \"description\" argument must not be empty and must consist of no more than {DESCRIPTION_MAX_LENGTH} characters.");

            var requisite = new RequisiteForAssistance(title, description);

            return requisite;
        }
    }
}