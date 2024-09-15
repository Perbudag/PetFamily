﻿using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Domain.VolunteerAggregate.Enums;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class Volunteer : Shared.Models.Entity<VolunteerId>
    {
        public FullName FullName { get; private set; }
        public Description Description { get; private set; }
        public WorkExperience WorkExperience { get; private set; }
        public EmailAddress Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public ValueObjectList<SocialNetwork> SocialNetworks { get; private set; }
        public ValueObjectList<Requisite> Requisites { get; private set; }
        public ValueObjectList<Pet> Pets { get; private set; }

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
            List<SocialNetwork> socialNetworks,
            List<Requisite> requisites) : base(VolunteerId.NewId())
        {
            FullName = fullName;
            Description = description;
            WorkExperience = workExperience;
            Email = email;
            PhoneNumber = phoneNumber;
            SocialNetworks = socialNetworks;
            Requisites = requisites;
        }
    }
}
