using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.SpeciesAggregate.Entities;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.SpeciesAggregate
{
    public class Species : Entity<SpeciesId>
    {
        private readonly List<Breed> _Breeds = [];

        public Name Name { get; private set; }
        public IReadOnlyList<Breed> Breeds => _Breeds.AsReadOnly();

        private Species() : base(SpeciesId.NewId()) { }
        public Species(Name name) : base(SpeciesId.NewId())
        {
            Name = name;
        }
    }
}
