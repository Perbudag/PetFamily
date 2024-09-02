using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Infrastructure.Extensions;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Infrastructure.Database.Configurations
{
    internal class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");


            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .IsRequired()
                .HasConversion(
                id => id.Value,
                id => VolunteerId.Create(id));


            builder.ComplexProperty(v => v.FullName, b =>
            {
                b.Property(fn => fn.Firstname)
                .HasColumnName("firstname")
                .IsRequired()
                .HasMaxLength(FullName.FIRSTNAME_MAX_LENGTH);

                b.Property(fn => fn.Lastname)
                .HasColumnName("lastname")
                .IsRequired()
                .HasMaxLength(FullName.LASTNAME_MAX_LENGTH);

                b.Property(fn => fn.Patronymic)
                .HasColumnName("patronymic")
                .IsRequired(false)
                .HasMaxLength(FullName.PATRONYMIC_MAX_LENGTH);
            });


            builder.Property(v => v.Description)
                .IsRequired(false)
                .HasMaxLength(Volunteer.DESCRIPTION_MAX_LENGTH);

            builder.ComplexProperty(v => v.WorkExperienceDetails, b =>
            {
                b.Property(we => we.WorkExperience)
                .HasColumnName("work_experience")
                .IsRequired()
                .HasDefaultValue(0);

                b.Property(we => we.PetsFoundHomeCount)
                .HasColumnName("pets_found_home_count")
                .IsRequired()
                .HasDefaultValue(0);

                b.Property(we => we.PetsLookingForHomeCount)
                .HasColumnName("pets_looking_forHome_count")
                .IsRequired()
                .HasDefaultValue(0);

                b.Property(we => we.PetsOnTreatmentCount)
                .HasColumnName("pets_on_treatment_count")
                .IsRequired()
                .HasDefaultValue(0);
            });

            builder.Property(v => v.Email)
               .IsRequired()
               .HasConversion(
                   e => e.ToString(),
                   e => EmailAddress.Parse(e).Value)
               .HasMaxLength(EmailAddress.EMAIL_MAX_LENGTH);


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
