using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.SpeciesAggregate.Entities
{
    public class Breed : Entity<BreedId>
    {
        public Name Name { get; private set; }


        private Breed() : base(BreedId.NewId()) { }
        public Breed(Name name) : base(BreedId.NewId())
        {
            Name = name;
        }
    }
}
