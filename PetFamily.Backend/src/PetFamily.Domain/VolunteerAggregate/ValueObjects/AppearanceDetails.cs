using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record AppearanceDetails
    {
        public const int SPECIES_MAX_LENGTH = 50;
        public const int BREED_MAX_LENGTH = 50;
        public const int COLORATION_MAX_LENGTH = 100;

        public string Species { get; }
        public string Breed { get; }
        public string Coloration { get; }
        public float Weight { get; }
        public float Height { get; }

        public AppearanceDetails(string species, string breed, string coloration, float weight, float height)
        {
            Species = species;
            Breed = breed;
            Coloration = coloration;
            Weight = weight;
            Height = height;
        }

        public static Result<AppearanceDetails> Creeate(string species, string breed, string coloration, float weight, float height)
        {
            if (string.IsNullOrWhiteSpace(species) || species.Length > SPECIES_MAX_LENGTH)
                return Result.Failure<AppearanceDetails>($"The \"species\" argument must not be empty and must consist of no more than {SPECIES_MAX_LENGTH} characters.");
            
            if (string.IsNullOrWhiteSpace(breed) || breed.Length > BREED_MAX_LENGTH)
                return Result.Failure<AppearanceDetails>($"The \"breed\" argument must not be empty and must consist of no more than {BREED_MAX_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(coloration) || coloration.Length > COLORATION_MAX_LENGTH)
                return Result.Failure<AppearanceDetails>($"The \"coloration\" argument must not be empty and must consist of no more than {COLORATION_MAX_LENGTH} characters.");

            if (weight <= 0)
                return Result.Failure<AppearanceDetails>($"The \"weight\" argument must be greater than zero.");

            if (height <= 0)
                return Result.Failure<AppearanceDetails>($"The \"height\" argument must be greater than zero.");

            var appearanceDetails = new AppearanceDetails(species, breed, coloration, weight, height);

            return Result.Success(appearanceDetails);
        }
    }
}
