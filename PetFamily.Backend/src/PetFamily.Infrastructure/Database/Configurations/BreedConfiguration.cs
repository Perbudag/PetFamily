using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.SpeciesAggregate.Entities;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Infrastructure.Database.Configurations
{
    internal class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breeds");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    id => BreedId.Create(id));


            builder.ComplexProperty(p => p.Name, b =>
            {
                b.Property(p => p.Value)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Name.NAME_MAX_LENGTH);
            });
        }
    }
}
