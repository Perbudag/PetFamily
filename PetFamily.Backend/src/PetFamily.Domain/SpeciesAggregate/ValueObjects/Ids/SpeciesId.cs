using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids
{
    public record SpeciesId
    {
        public Guid Value { get; }

        private SpeciesId(Guid value)
        {
            Value = value;
        }

        public static SpeciesId NewId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);
    }
}