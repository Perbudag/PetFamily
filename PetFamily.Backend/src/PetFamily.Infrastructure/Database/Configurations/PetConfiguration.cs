using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Infrastructure.Extensions;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;
using PetFamily.Domain.SpeciesAggregate.ValueObjects.Ids;

namespace PetFamily.Infrastructure.Database.Configurations
{
    internal class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    id => PetId.Create(id));

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Pet.NAME_MAX_LENGTH);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(Pet.DESCRIPTION_MAX_LENGTH);

            builder.ComplexProperty(p => p.AppearanceDetails, b =>
            {
                b.Property(a => a.SpeciesId)
                .HasColumnName("species_id")
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    id => SpeciesId.Create(id));

                b.Property(a => a.BreedId)
                .HasColumnName("breed_id")
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    id => BreedId.Create(id));

                b.Property(a => a.Coloration)
                .HasColumnName("coloration")
                    .IsRequired()
                    .HasMaxLength(AppearanceDetails.COLORATION_MAX_LENGTH);

                b.Property(a => a.Weight)
                .HasColumnName("weight")
                .IsRequired();

                b.Property(a => a.Height)
                .HasColumnName("height")
                    .IsRequired();
            });

            builder.ComplexProperty(p => p.HealthDetails, b =>
            {
                b.Property(hd => hd.Description)
                .HasColumnName("health_description")
                .IsRequired()
                .HasMaxLength(HealthDetails.DESCRIPTION_MAX_LENGTH);

                b.Property(hd => hd.IsCastrated)
                .HasColumnName("is_castrated")
                .IsRequired();

                b.Property(hd => hd.IsVaccinated)
                .HasColumnName("is_vaccinated")
                .IsRequired();
            });

            builder.Property(p => p.ResidentialAddress)
                .IsRequired()
                .HasConversion(
                    a => a.ToString(),
                    a => MapAddress.Parse(a).Value)
                .HasMaxLength(MapAddress.ADDRESS_MAX_LENGTH);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasColumnType($"char({Pet.PHONE_NUMBER_LENGTH})");

            builder.Property(p => p.AssistanceStatus)
                .IsRequired()
                .HasConversion<string>();

            builder.ValueObjectListToJson(p => p.Requisites, RequisitesBuilder =>
            {
                RequisitesBuilder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(RequisiteForAssistance.TITLE_MAX_LENGTH);

                RequisitesBuilder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(RequisiteForAssistance.DESCRIPTION_MAX_LENGTH);
            });

            builder.ValueObjectListToJson(p => p.Photos, PhotosBuilder =>
            {
                PhotosBuilder.Property(ph => ph.Path)
                .IsRequired();

                PhotosBuilder.Property<bool>(ph => ph.IsMain)
                .IsRequired();
            });
        }
    }
}
