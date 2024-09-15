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
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("description", DESCRIPTION_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;

            var healthDetails = new HealthDetails(description, isCastrated, isVaccinated);

            return healthDetails;
        }
    }
}
