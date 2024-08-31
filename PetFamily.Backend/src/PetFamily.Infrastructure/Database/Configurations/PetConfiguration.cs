using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Infrastructure.Extensions;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Infrastructure.Database.Configurations
{
    internal class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Pet.NAME_MAX_LENGTH);

            builder.Property(p => p.Species)
                .IsRequired()
                .HasMaxLength(Pet.SPECIES_MAX_LENGTH);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(Pet.DESCRIPTION_MAX_LENGTH);

            builder.Property(p => p.Breed)
                .IsRequired()
                .HasMaxLength(Pet.BREED_MAX_LENGTH);

            builder.Property(p => p.Coloration)
                .IsRequired()
                .HasMaxLength(Pet.COLORATION_MAX_LENGTH);

            builder.Property(p => p.HealthInformation)
                .IsRequired()
                .HasMaxLength(Pet.HEALTH_INFORMATION_MAX_LENGTH);

            builder.Property(p => p.ResidentialAddress)
                .IsRequired();

            builder.Property(p => p.Weight)
                .IsRequired();

            builder.Property(p => p.Height)
                .IsRequired();

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
