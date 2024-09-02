using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record WorkExperienceDetails
    {
        public int WorkExperience { get; }
        public int PetsFoundHomeCount { get; }
        public int PetsLookingForHomeCount { get; }
        public int PetsOnTreatmentCount { get; }

        private WorkExperienceDetails(int workExperience, int petsFoundHomeCount, int petsLookingForHomeCount, int petsOnTreatmentCount)
        {
            WorkExperience = workExperience;
            PetsFoundHomeCount = petsFoundHomeCount;
            PetsLookingForHomeCount = petsLookingForHomeCount;
            PetsOnTreatmentCount = petsOnTreatmentCount;
        }

        public static Result<WorkExperienceDetails> Create(int workExperience, int petsFoundHomeCount, int petsLookingForHomeCount, int petsOnTreatmentCount)
        {
            var workExperienceDetails = new WorkExperienceDetails(workExperience, petsFoundHomeCount, petsLookingForHomeCount, petsOnTreatmentCount);

            return Result.Success(workExperienceDetails);
        }
    }
}
