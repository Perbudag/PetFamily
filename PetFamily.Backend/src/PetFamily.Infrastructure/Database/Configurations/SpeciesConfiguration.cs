using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.SpeciesAggregate;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Infrastructure.Database.Configurations
{
    internal class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    id => SpeciesId.Create(id));


            builder.Property(p => p.Name)
                .IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => Name.Create(v).Value)
                .HasMaxLength(Name.NAME_MAX_LENGTH);


            builder.HasMany(v => v.Breeds)
                .WithOne()
                .HasForeignKey("species_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
