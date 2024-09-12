using PetFamily.Domain.Shared.Models;
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

        public static Result<AppearanceDetails> Create(SpeciesId speciesId, BreedId breedId, string coloration, float weight, float height)
        {

            if (speciesId == null)
                return Error.Validation("AppearanceDetails.Create.Invalid", $"The \"speciesId\" argument must not be empty.");

            if (breedId == null)
                return Error.Validation("AppearanceDetails.Create.Invalid", $"The \"breedId\" argument must not be empty.");

            if (string.IsNullOrWhiteSpace(coloration) || coloration.Length > COLORATION_MAX_LENGTH)
                return Error.Validation("AppearanceDetails.Create.Invalid", $"The \"coloration\" argument must not be empty and must consist of no more than {COLORATION_MAX_LENGTH} characters.");

            if (weight <= 0)
                return Error.Validation("AppearanceDetails.Create.Invalid", $"The \"weight\" argument must be greater than zero.");

            if (height <= 0)
                return Error.Validation("AppearanceDetails.Create.Invalid", $"The \"height\" argument must be greater than zero.");

            var appearanceDetails = new AppearanceDetails(speciesId, breedId, coloration, weight, height);

            return appearanceDetails;
        }
    }
}
