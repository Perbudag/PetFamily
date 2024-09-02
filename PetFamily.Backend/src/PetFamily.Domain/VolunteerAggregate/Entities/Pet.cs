using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.VolunteerAggregate.Enums;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;

namespace PetFamily.Domain.VolunteerAggregate.Entities
{
    public class Pet : Shared.Models.Entity<PetId>
    {
        public const int NAME_MAX_LENGTH = 50;
        public const int DESCRIPTION_MAX_LENGTH = 500;
        public const int PHONE_NUMBER_LENGTH = 11;

        private readonly List<RequisiteForAssistance> _Requisites = [];
        private readonly List<PetPhoto> _Photos = [];

        public string Name { get; private set; }
        public string Description { get; private set; }
        public AppearanceDetails AppearanceDetails { get; private set; }
        public HealthDetails HealthDetails { get; private set; }
        public MapAddress ResidentialAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public AssistanceStatus AssistanceStatus { get; private set; }
        public ValueObjectList<RequisiteForAssistance> Requisites => _Requisites;
        public ValueObjectList<PetPhoto> Photos => _Photos;
        public DateTime CreatedAt { get; private set; }

        private Pet() : base(PetId.NewId()) {}
        private Pet(
            string name,
            string description,
            AppearanceDetails appearanceDetails,
            HealthDetails healthDetails,
            MapAddress residentialAddress,
            string phoneNumber,
            DateTime dateOfBirth,
            AssistanceStatus assistanceStatus,
            List<RequisiteForAssistance> requisites,
            List<PetPhoto> photos) : base(PetId.NewId())
        {
            Name = name;
            Description = description;
            AppearanceDetails = appearanceDetails;
            HealthDetails = healthDetails;
            ResidentialAddress = residentialAddress;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            AssistanceStatus = assistanceStatus;
            _Requisites = requisites;
            _Photos = photos;
            CreatedAt = DateTime.UtcNow;
        }

        public static Result<Pet> Creeate(
            string name,
            string description,
            AppearanceDetails appearanceDetails,
            HealthDetails healthDetails,
            MapAddress residentialAddress,
            string phoneNumber,
            DateTime dateOfBirth,
            AssistanceStatus assistanceStatus,
            List<RequisiteForAssistance> requisites,
            List<PetPhoto> photos)
        {
            if(string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"name\" argument must not be empty and must consist of no more than {NAME_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"description\" argument must not be empty and must consist of no more than {DESCRIPTION_MAX_LENGTH} characters.");

            if (appearanceDetails == null)
                return Result.Failure<Pet>($"The \"appearanceDetails\" argument must not be empty.");

            if (healthDetails == null)
                return Result.Failure<Pet>($"The \"healthDetails\" argument must not be empty.");

            if (residentialAddress == null)
                return Result.Failure<Pet>($"The \"residentialAddress\" argument must not be empty.");

            if(phoneNumber.Length != PHONE_NUMBER_LENGTH)
                return Result.Failure<Pet>($"The \"phoneNumber\" argument must be {PHONE_NUMBER_LENGTH} characters long.");

            var pet = new Pet(name,
                              description, 
                              appearanceDetails,
                              healthDetails,
                              residentialAddress,
                              phoneNumber,
                              dateOfBirth,
                              assistanceStatus,
                              requisites,
                              photos);

            return Result.Success(pet);
        }
    }
}
