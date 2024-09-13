using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.SpeciesAggregate.Entities
{
    public class Breed : Shared.Models.Entity<BreedId>
    {
        public const int NAME_MAX_LENGTH = 50;


        public string Name { get; private set; }


        private Breed() : base(BreedId.NewId()) { }
        private Breed(string name) : base(BreedId.NewId())
        {
            Name = name;
        }


        public static Result<Breed> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                return Error.Validation("Breed.Create.Invalid", $"The \"name\" argument must not be empty and must consist of no more than {NAME_MAX_LENGTH} characters.");

            var breed = new Breed(name);

            return breed;
        }
    }
}
