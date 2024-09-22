using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.Enums;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate.Entities
{
    public class Pet : Entity<PetId>, ISoftDeletable
    {
        private bool _IsDeleted = false;

        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public AppearanceDetails AppearanceDetails { get; private set; }
        public HealthDetails HealthDetails { get; private set; }
        public MapAddress ResidentialAddress { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public AssistanceStatus AssistanceStatus { get; private set; }
        public ValueObjectList<Requisite> Requisites { get; private set; }
        public ValueObjectList<PetPhoto> Photos { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Pet() : base(PetId.NewId()) {}
        public Pet(
            Name name,
            Description description,
            AppearanceDetails appearanceDetails,
            HealthDetails healthDetails,
            MapAddress residentialAddress,
            PhoneNumber phoneNumber,
            DateTime dateOfBirth,
            AssistanceStatus assistanceStatus,
            ValueObjectList<Requisite> requisites,
            ValueObjectList<PetPhoto> photos) : base(PetId.NewId())
        {
            Name = name;
            Description = description;
            AppearanceDetails = appearanceDetails;
            HealthDetails = healthDetails;
            ResidentialAddress = residentialAddress;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            AssistanceStatus = assistanceStatus;
            Requisites = requisites;
            Photos = photos;
            CreatedAt = DateTime.UtcNow;
        }

        public void Delete() =>
            _IsDeleted = true;

        public void Restore() =>
            _IsDeleted = false;

        public bool IsDeleted() =>
            _IsDeleted;
    }
}
