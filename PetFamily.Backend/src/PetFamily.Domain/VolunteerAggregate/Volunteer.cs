using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Domain.VolunteerAggregate.Enums;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class Volunteer : Entity<VolunteerId>
    {
        private readonly List<Pet> _Pets = [];

        public FullName FullName { get; private set; }
        public Description Description { get; private set; }
        public WorkExperience WorkExperience { get; private set; }
        public EmailAddress Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public ValueObjectList<SocialNetwork> SocialNetworks { get; private set; }
        public ValueObjectList<Requisite> Requisites { get; private set; }
        public IReadOnlyList<Pet> Pets => _Pets.AsReadOnly();

        public int PetsFoundHomeCount => Pets.Count(p => p.AssistanceStatus == AssistanceStatus.FoundTheHouse);
        public int PetsLookingForHomeCount => Pets.Count(p => p.AssistanceStatus == AssistanceStatus.LookingForAHome);
        public int PetsNeedsHelpCount => Pets.Count(p => p.AssistanceStatus == AssistanceStatus.NeedsHelp);

        private Volunteer() : base(VolunteerId.NewId()) {}
        public Volunteer(
            FullName fullName,
            Description description,
            WorkExperience workExperience,
            EmailAddress email,
            PhoneNumber phoneNumber,
            ValueObjectList<SocialNetwork> socialNetworks,
            ValueObjectList<Requisite> requisites) : base(VolunteerId.NewId())
        {
            FullName = fullName;
            Description = description;
            WorkExperience = workExperience;
            Email = email;
            PhoneNumber = phoneNumber;
            SocialNetworks = socialNetworks;
            Requisites = requisites;
        }

        public void UpdateMainInfo(
            FullName fullName,
            Description description,
            WorkExperience workExperience,
            EmailAddress email,
            PhoneNumber phoneNumber)
        {
            FullName = fullName;
            Description = description;
            WorkExperience = workExperience;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void UpdateSocialNetworks(ValueObjectList<SocialNetwork> socialNetworks)=>
            SocialNetworks = socialNetworks;

        public void UpdateRequisites(ValueObjectList<Requisite> requisites) =>
            Requisites = requisites;
    }
}
