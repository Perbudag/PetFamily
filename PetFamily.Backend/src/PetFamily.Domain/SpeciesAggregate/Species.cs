using CSharpFunctionalExtensions;
using PetFamily.Domain.SpeciesAggregate.Entities;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.SpeciesAggregate
{
    public class Species : Shared.Models.Entity<SpeciesId>
    {
        public const int NAME_MAX_LENGTH = 50;

        private readonly List<Breed> _Breeds = [];

        public string Name { get; private set; }
        public IReadOnlyList<Breed> Breeds => _Breeds.AsReadOnly();

        private Species() : base(SpeciesId.NewId()) { }
        private Species(string name, List<Breed> breeds) : base(SpeciesId.NewId())
        {
            Name = name;
            _Breeds = breeds;
        }

        public static Result<Species> Create(string name, List<Breed> breeds)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                return Result.Failure<Species>($"The \"name\" argument must not be empty and must consist of no more than {NAME_MAX_LENGTH} characters.");

            var species = new Species(name, breeds);

            return Result.Success(species);
        }
    }
}
