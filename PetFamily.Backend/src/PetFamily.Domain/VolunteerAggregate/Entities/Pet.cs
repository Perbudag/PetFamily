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
        public const int SPECIES_MAX_LENGTH = 50;
        public const int DESCRIPTION_MAX_LENGTH = 500;
        public const int BREED_MAX_LENGTH = 50;
        public const int COLORATION_MAX_LENGTH = 100;
        public const int HEALTH_INFORMATION_MAX_LENGTH = 1000;
        public const int PHONE_NUMBER_LENGTH = 11;

        private readonly List<RequisiteForAssistance> _Requisites;
        private readonly List<PetPhoto> _Photos;

        public string Name { get; private set; }
        public string Species { get; private set; }
        public string Description { get; private set; }
        public string Breed { get; private set; }
        public string Coloration { get; private set; }
        public string HealthInformation { get; private set; }
        public MapAddress ResidentialAddress { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool IsCastrated { get; private set; }
        public bool IsVaccinated { get; private set; }
        public AssistanceStatus AssistanceStatus { get; private set; }
        public ValueObjectList<RequisiteForAssistance> Requisites => _Requisites;
        public ValueObjectList<PetPhoto> Photos => _Photos;
        public DateTime CreatedAt { get; private set; }

        private Pet() : base(PetId.NewId()) {}
        private Pet(string name,
                   string species,
                   string description,
                   string breed,
                   string coloration,
                   string healthInformation,
                   MapAddress residentialAddress,
                   float weight,
                   float height,
                   string phoneNumber,
                   DateTime dateOfBirth,
                   bool isCastrated,
                   bool isVaccinated,
                   AssistanceStatus assistanceStatus,
                   List<RequisiteForAssistance> requisites,
                   List<PetPhoto> photos) : base(PetId.NewId())
        {
            Name = name;
            Species = species;
            Description = description;
            Breed = breed;
            Coloration = coloration;
            HealthInformation = healthInformation;
            ResidentialAddress = residentialAddress;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            IsCastrated = isCastrated;
            IsVaccinated = isVaccinated;
            AssistanceStatus = assistanceStatus;
            _Requisites = requisites;
            _Photos = photos;
            CreatedAt = DateTime.UtcNow;
        }

        public static Result<Pet> Creeate(string name,
                                  string species,
                                  string description,
                                  string breed,
                                  string coloration,
                                  string healthInformation,
                                  MapAddress residentialAddress,
                                  float weight,
                                  float height,
                                  string phoneNumber,
                                  DateTime dateOfBirth,
                                  bool isCastrated,
                                  bool isVaccinated,
                                  AssistanceStatus assistanceStatus,
                                  List<RequisiteForAssistance> requisites,
                                  List<PetPhoto> photos)
        {
            if(string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"name\" argument must not be empty and must consist of no more than {NAME_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(species) || species.Length > SPECIES_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"species\" argument must not be empty and must consist of no more than {SPECIES_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"description\" argument must not be empty and must consist of no more than {DESCRIPTION_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(breed) || breed.Length > BREED_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"breed\" argument must not be empty and must consist of no more than {BREED_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(coloration) || coloration.Length > COLORATION_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"coloration\" argument must not be empty and must consist of no more than {COLORATION_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(healthInformation) || healthInformation.Length > HEALTH_INFORMATION_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"healthInformation\" argument must not be empty and must consist of no more than {HEALTH_INFORMATION_MAX_LENGTH} characters.");

            if(residentialAddress == null)
                return Result.Failure<Pet>($"The \"residentialAddress\" argument must not be empty.");

            if(weight <= 0)
                return Result.Failure<Pet>($"The \"weight\" argument must be greater than zero.");

            if(height <= 0)
                return Result.Failure<Pet>($"The \"height\" argument must be greater than zero.");

            if(phoneNumber.Length != PHONE_NUMBER_LENGTH)
                return Result.Failure<Pet>($"The \"phoneNumber\" argument must be {PHONE_NUMBER_LENGTH} characters long.");

            var pet = new Pet(name,
                              species,
                              description,
                              breed,
                              coloration,
                              healthInformation,
                              residentialAddress,
                              weight,
                              height,
                              phoneNumber,
                              dateOfBirth,
                              isCastrated,
                              isVaccinated,
                              assistanceStatus,
                              requisites,
                              photos);

            return Result.Success(pet);
        }
    }
}
