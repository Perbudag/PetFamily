using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.VolunteerAggregate.Entities;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class Volunteer : Shared.Models.Entity<VolunteerId>
    {
        public const int DESCRIPTION_MAX_LENGTH = 500;
        public const int PHONE_NUMBER_LENGTH = 11;


        private readonly List<SocialNetwork> _SocialNetworks = [];
        private readonly List<RequisiteForAssistance> _Requisites = [];
        private readonly List<Pet> _Pets = [];

        public FullName FullName { get; private set; }
        public string Description { get; private set; }
        public WorkExperienceDetails WorkExperienceDetails { get; private set; }
        public EmailAddress Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public ValueObjectList<SocialNetwork> SocialNetworks => _SocialNetworks;
        public ValueObjectList<RequisiteForAssistance> Requisites => _Requisites;
        public ValueObjectList<Pet> Pets => _Pets;

        private Volunteer() : base(VolunteerId.NewId()) {}
        private Volunteer(
            FullName fullName,
            string description,
            WorkExperienceDetails workExperienceDetails,
            EmailAddress email,
            string phoneNumber,
            List<SocialNetwork> socialNetworks,
            List<RequisiteForAssistance> requisites,
            List<Pet> pets) : base(VolunteerId.NewId())
        {
            FullName = fullName;
            Description = description;
            WorkExperienceDetails = workExperienceDetails;
            Email = email;
            PhoneNumber = phoneNumber;
            _SocialNetworks = socialNetworks;
            _Requisites = requisites;
            _Pets = pets;
            WorkExperienceDetails = workExperienceDetails;
        }

        public static Result<Volunteer> Create(
            FullName fullName,
            string description,
            WorkExperienceDetails workExperienceDetails,
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

            if (workExperienceDetails == null)
                return Result.Failure<Volunteer>($"The \"workExperienceDetails\" argument must not be empty.");

            if (email == null)
                return Result.Failure<Volunteer>($"The \"email\" argument must not be empty.");

            if (phoneNumber.Length != PHONE_NUMBER_LENGTH)
                return Result.Failure<Volunteer>($"The \"phoneNumber\" argument must be {PHONE_NUMBER_LENGTH} characters long.");


            var volunteer = new Volunteer(fullName,
                                          description,
                                          workExperienceDetails,
                                          email,
                                          phoneNumber,
                                          socialNetworks,
                                          requisites,
                                          pets);

            return Result.Success(volunteer);
        }

    }
}
