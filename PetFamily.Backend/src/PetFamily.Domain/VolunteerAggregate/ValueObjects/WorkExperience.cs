using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record WorkExperience
    {
        public int Value { get; }

        private WorkExperience(int value)
        {
            Value = value;
        }

        public static Result<WorkExperience> Create(int workExperience)
        {
            List<Error> errors = [];

            if (workExperience < 0)
                errors.Add(Errors.General.Validation.Int.MustBePositive("workExperience"));

            if (errors.Count > 0)
                return errors;

            return new WorkExperience(workExperience);
        }
    }
}
