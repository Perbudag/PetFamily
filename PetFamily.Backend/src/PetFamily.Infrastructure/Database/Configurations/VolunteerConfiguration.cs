using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Infrastructure.Extensions;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;
using PetFamily.Domain.Shared.ValueObjects;

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


            builder.ComplexProperty(v => v.Description, b =>
            {
                b.Property(d => d.Value)
                .HasColumnName("description")
                .IsRequired(false)
                .HasMaxLength(Description.DESCRIPTION_MAX_LENGTH);
            });


            builder.ComplexProperty(v => v.WorkExperience, b =>
            {
                b.Property(we => we.Value)
                .HasColumnName("work_experience")
                .IsRequired()
                .HasDefaultValue(0);
            });


            builder.Property(v => v.Email)
               .IsRequired()
               .HasConversion(
                   e => e.ToString(),
                   e => EmailAddress.Parse(e).Value)
               .HasMaxLength(EmailAddress.EMAIL_MAX_LENGTH);


            builder.ComplexProperty(v => v.PhoneNumber, b =>
            {
                b.Property(v => v.Value)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasColumnType($"char({PhoneNumber.PHONE_NUMBER_LENGTH})");
            });


            builder.ValueObjectListToJson(v => v.SocialNetworks, SocialNetworkBuilder =>
            {
                SocialNetworkBuilder.Property(sn => sn.Name)
                .IsRequired()
                .HasMaxLength(SocialNetwork.NAME_MAX_LENGTH);

                SocialNetworkBuilder.Property(sn => sn.Path)
                .IsRequired()
                .HasMaxLength(SocialNetwork.PATH_MAX_LENGTH);
            });

            builder.ValueObjectListToJson(v => v.Requisites, RequisitesBuilder =>
            {
                RequisitesBuilder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(Requisite.TITLE_MAX_LENGTH);

                RequisitesBuilder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(Requisite.DESCRIPTION_MAX_LENGTH);
            });



            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
