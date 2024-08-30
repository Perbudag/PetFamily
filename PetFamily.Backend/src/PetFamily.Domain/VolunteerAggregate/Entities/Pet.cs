using CSharpFunctionalExtensions;
using PetFamily.Domain.VolunteerAggregate.Enums;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate.Entities
{
    public class Pet
    {
        public static readonly int NAME_MAX_LENGTH = 50;
        public static readonly int DESCRIPTION_MAX_LENGTH = 500;
        public static readonly int HEALTH_INFORMATION_MAX_LENGTH = 1000;
        public static readonly int PHONE_NUMBER_LENGTH = 11;

        private List<RequisiteForAssistance> _Requisites;
        private List<PetPhoto> _Photos;

        private Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string HealthInformation { get; private set; }
        public string ResidentialAddress { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool IsCastrated { get; private set; }
        public bool IsVaccinated { get; private set; }
        public AssistanceStatus AssistanceStatus { get; private set; }
        public IReadOnlyList<RequisiteForAssistance> Requisites => _Requisites.AsReadOnly();
        public IReadOnlyList<PetPhoto> Photos => _Photos.AsReadOnly();
        public DateTime CreatedAt { get; private set; }

        private Pet() {}
        private Pet(string name,
                   string description,
                   string healthInformation,
                   string residentialAddress,
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
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
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
                                  string description,
                                  string healthInformation,
                                  string residentialAddress,
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

            if(string.IsNullOrWhiteSpace(description) || description.Length > DESCRIPTION_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"description\" argument must not be empty and must consist of no more than {DESCRIPTION_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(healthInformation) || healthInformation.Length > HEALTH_INFORMATION_MAX_LENGTH)
                return Result.Failure<Pet>($"The \"healthInformation\" argument must not be empty and must consist of no more than {HEALTH_INFORMATION_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(residentialAddress))
                return Result.Failure<Pet>($"The \"residentialAddress\" argument must not be empty.");

            if(weight <= 0)
                return Result.Failure<Pet>($"The \"weight\" argument must be greater than zero.");

            if(height <= 0)
                return Result.Failure<Pet>($"The \"height\" argument must be greater than zero.");

            if(phoneNumber.Length != PHONE_NUMBER_LENGTH)
                return Result.Failure<Pet>($"The \"phoneNumber\" argument must be {PHONE_NUMBER_LENGTH} characters long.");

            var pet = new Pet(name,
                              description,
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
