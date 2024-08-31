using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Infrastructure.Extensions;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Infrastructure.Database.Configurations
{
    internal class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.ComplexProperty(v => v.FullName, fullnameBuilder =>
            {
                fullnameBuilder.Property(fn => fn.Firstname)
                .HasColumnName("firstname")
                .IsRequired()
                .HasMaxLength(FullName.FIRSTNAME_MAX_LENGTH);

                fullnameBuilder.Property(fn => fn.Lastname)
                .HasColumnName("lastname")
                .IsRequired()
                .HasMaxLength(FullName.LASTNAME_MAX_LENGTH);

                fullnameBuilder.Property(fn => fn.Patronymic)
                .HasColumnName("patronymic")
                .IsRequired(false)
                .HasMaxLength(FullName.PATRONYMIC_MAX_LENGTH);
            });

            builder.Property(v => v.Email)
                .IsRequired()
                .HasMaxLength(Volunteer.EMAIL_MAX_LENGTH);

            builder.Property(v => v.Description)
                .IsRequired(false)
                .HasMaxLength(Volunteer.DESCRIPTION_MAX_LENGTH);

            builder.Property(v => v.PhoneNumber)
                .IsRequired()
                .HasColumnType($"char({Volunteer.PHONE_NUMBER_LENGTH})");


            builder.ValueObjectListToJson(v => v.SocialNetworks, SocialNetworkBuilder =>
            {
                SocialNetworkBuilder.Property(sn => sn.Name)
                .IsRequired()
                .HasMaxLength(SocialNetwork.NAME_MAX_LENGTH);

                SocialNetworkBuilder.Property(sn => sn.Path)
                .IsRequired();
            });

            builder.ValueObjectListToJson(v => v.Requisites, RequisitesBuilder =>
            {
                RequisitesBuilder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(RequisiteForAssistance.TITLE_MAX_LENGTH);

                RequisitesBuilder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(RequisiteForAssistance.DESCRIPTION_MAX_LENGTH);
            });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
