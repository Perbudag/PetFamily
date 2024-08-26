using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Common.ValueObjects
{
    public record RequisiteForAssistance
    {
        public static readonly int TITLE_MAX_LENGTH = 250;
        public static readonly int DESCRIPTION_MAX_LENGTH = 500;


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
                return Result.Failure<RequisiteForAssistance>($"The \"title\" argument must not be empty and must consist of no more than {TITLE_MAX_LENGTH} characters.");
            
            if(string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                return Result.Failure<RequisiteForAssistance>($"The \"description\" argument must not be empty and must consist of no more than {DESCRIPTION_MAX_LENGTH} characters.");

            var requisite = new RequisiteForAssistance(title, description);

            return Result.Success(requisite);
        }
    }
}