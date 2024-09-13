using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record HealthDetails
    {
        public const int DESCRIPTION_MAX_LENGTH = 500;

        public string Description { get; }
        public bool IsCastrated { get; }
        public bool IsVaccinated { get; }

        private HealthDetails(string description, bool isCastrated, bool isVaccinated)
        {
            Description = description;
            IsCastrated = isCastrated;
            IsVaccinated = isVaccinated;
        }

        public static Result<HealthDetails> Create(string description, bool isCastrated = false, bool isVaccinated = false)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                return Error.Validation("HealthDetails.Create.Invalid", $"The \"description\" argument must not be empty and must consist of no more than {DESCRIPTION_MAX_LENGTH} characters.");

            var healthDetails = new HealthDetails(description, isCastrated, isVaccinated);

            return healthDetails;
        }
    }
}
