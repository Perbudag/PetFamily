using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Domain.VolunteerAggregate.Enums;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class Volunteer : Shared.Models.Entity<VolunteerId>
    {
        public const int DESCRIPTION_MAX_LENGTH = 500;
        public const int PHONE_NUMBER_LENGTH = 11;

        public FullName FullName { get; private set; }
        public string Description { get; private set; }
        public int WorkExperience { get; private set; }
        public EmailAddress Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public ValueObjectList<SocialNetwork> SocialNetworks { get; private set; }
        public ValueObjectList<RequisiteForAssistance> Requisites { get; private set; }
        public ValueObjectList<Pet> Pets { get; private set; }

        public int PetsFoundHomeCount => Pets.Count(p => p.AssistanceStatus == AssistanceStatus.FoundTheHouse);
        public int PetsLookingForHomeCount => Pets.Count(p => p.AssistanceStatus == AssistanceStatus.LookingForAHome);
        public int PetsNeedsHelpCount => Pets.Count(p => p.AssistanceStatus == AssistanceStatus.NeedsHelp);

        private Volunteer() : base(VolunteerId.NewId()) {}
        private Volunteer(
            FullName fullName,
            string description,
            int workExperience,
            EmailAddress email,
            string phoneNumber,
            List<SocialNetwork> socialNetworks,
            List<RequisiteForAssistance> requisites,
            List<Pet> pets) : base(VolunteerId.NewId())
        {
            FullName = fullName;
            Description = description;
            WorkExperience = workExperience;
            Email = email;
            PhoneNumber = phoneNumber;
            SocialNetworks = socialNetworks;
            Requisites = requisites;
            Pets = pets;
        }

        public static Result<Volunteer> Create(
            FullName fullName,
            string description,
            int workExperience,
            EmailAddress email,
            string phoneNumber,
            List<SocialNetwork> socialNetworks,
            List<RequisiteForAssistance> requisites,
            List<Pet> pets)
        {
            if (fullName == null)
                return Result.Failure<Volunteer>($"The \"fullName\" argument must not be empty.");

            if (description.Length > DESCRIPTION_MAX_LENGTH)
                return Result.Failure<Volunteer>($"The \"description\" argument must not contain more than {DESCRIPTION_MAX_LENGTH} characters");

            if (email == null)
                return Result.Failure<Volunteer>($"The \"email\" argument must not be empty.");

            if (phoneNumber.Length != PHONE_NUMBER_LENGTH)
                return Result.Failure<Volunteer>($"The \"phoneNumber\" argument must be {PHONE_NUMBER_LENGTH} characters long.");


            var volunteer = new Volunteer(fullName,
                                          description,
                                          workExperience,
                                          email,
                                          phoneNumber,
                                          socialNetworks,
                                          requisites,
                                          pets);

            return Result.Success(volunteer);
        }

    }
}
