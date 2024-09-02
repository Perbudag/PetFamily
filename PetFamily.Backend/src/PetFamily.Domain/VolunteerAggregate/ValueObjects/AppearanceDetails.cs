using CSharpFunctionalExtensions;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record AppearanceDetails
    {
        public const int COLORATION_MAX_LENGTH = 100;

        public SpeciesId SpeciesId { get; }
        public BreedId BreedId { get; }
        public string Coloration { get; }
        public float Weight { get; }
        public float Height { get; }

        public AppearanceDetails(SpeciesId speciesId, BreedId breedId, string coloration, float weight, float height)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
            Coloration = coloration;
            Weight = weight;
            Height = height;
        }

        public static Result<AppearanceDetails> Creeate(SpeciesId speciesId, BreedId breedId, string coloration, float weight, float height)
        {

            if (speciesId == null)
                return Result.Failure<AppearanceDetails>($"The \"speciesId\" argument must not be empty.");

            if (breedId == null)
                return Result.Failure<AppearanceDetails>($"The \"breedId\" argument must not be empty.");

            if (string.IsNullOrWhiteSpace(coloration) || coloration.Length > COLORATION_MAX_LENGTH)
                return Result.Failure<AppearanceDetails>($"The \"coloration\" argument must not be empty and must consist of no more than {COLORATION_MAX_LENGTH} characters.");

            if (weight <= 0)
                return Result.Failure<AppearanceDetails>($"The \"weight\" argument must be greater than zero.");

            if (height <= 0)
                return Result.Failure<AppearanceDetails>($"The \"height\" argument must be greater than zero.");

            var appearanceDetails = new AppearanceDetails(speciesId, breedId, coloration, weight, height);

            return Result.Success(appearanceDetails);
        }
    }
}
