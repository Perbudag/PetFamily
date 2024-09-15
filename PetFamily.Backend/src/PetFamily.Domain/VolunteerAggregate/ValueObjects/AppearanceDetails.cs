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
            List<Error> errors = [];

            if (speciesId == null)
                errors.Add(Errors.General.Validation.NotBeEmpty("speciesId"));

            if (breedId == null)
                errors.Add(Errors.General.Validation.NotBeEmpty("breedId"));

            if (string.IsNullOrWhiteSpace(coloration) || coloration.Length > COLORATION_MAX_LENGTH)
                errors.Add(Errors.General.Validation.String.NotBeEmptyAndNotBeLonger("coloration", COLORATION_MAX_LENGTH));

            if (weight <= 0)
                errors.Add(Errors.General.Validation.Int.MustBeGreaterThanZero("weight"));

            if (height <= 0)
                errors.Add(Errors.General.Validation.Int.MustBeGreaterThanZero("height"));

            if (errors.Count > 0)
                return errors;

            var appearanceDetails = new AppearanceDetails(speciesId!, breedId!, coloration, weight, height);

            return appearanceDetails;
        }
    }
}
