using CSharpFunctionalExtensions;
using PetFamily.Domain.Common.ValueObjects;
using PetFamily.Domain.PetAggregate;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class Volunteer
    {
        public static readonly int EMAIL_MAX_LENGTH = 320;
        public static readonly int DESCRIPTION_MAX_LENGTH = 500;
        public static readonly int PHONE_NUMBER_LENGTH = 11;


        private List<SocialNetwork> _SocialNetworks;
        private List<RequisiteForAssistance> _Requisites;
        private List<Pet> _Pets;

        public Guid Id { get; }
        public FullName FullName { get; private set; }
        public string Email { get; private set; }
        public string Description { get; private set; }
        public int YearsOfExperience { get; private set; }
        public int PetsFoundHomeCount { get; private set; }
        public int PetsLookingForHomeCount { get; private set; }
        public int PetsOnTreatmentCount { get; private set; }
        public string PhoneNumber { get; private set; }
        public IReadOnlyList<SocialNetwork> SocialNetworks => _SocialNetworks.AsReadOnly();
        public IReadOnlyList<RequisiteForAssistance> Requisites => _Requisites.AsReadOnly();
        public IReadOnlyList<Pet> Pets => _Pets.AsReadOnly();

        private Volunteer()
        {
            
        }
        private Volunteer(FullName fullName,
                          string email,
                          string description,
                          int yearsOfExperience,
                          int petsFoundHomeCount,
                          int petsLookingForHomeCount,
                          int petsOnTreatmentCount,
                          string phoneNumber,
                          List<SocialNetwork> socialNetworks,
                          List<RequisiteForAssistance> requisites,
                          List<Pet> pets)
        {

            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            Description = description;
            YearsOfExperience = yearsOfExperience;
            PetsFoundHomeCount = petsFoundHomeCount;
            PetsLookingForHomeCount = petsLookingForHomeCount;
            PetsOnTreatmentCount = petsOnTreatmentCount;
            PhoneNumber = phoneNumber;
            _SocialNetworks = socialNetworks;
            _Requisites = requisites;
            _Pets = pets;
        }

        public static Result<Volunteer> Create(FullName fullName,
                                               string email,
                                               string description,
                                               int yearsOfExperience,
                                               int petsFoundHomeCount,
                                               int petsLookingForHomeCount,
                                               int petsOnTreatmentCount,
                                               string phoneNumber,
                                               List<SocialNetwork> socialNetworks,
                                               List<RequisiteForAssistance> requisites,
                                               List<Pet> pets)
        {
            if(string.IsNullOrWhiteSpace(email) || email.Length > EMAIL_MAX_LENGTH)
                return Result.Failure<Volunteer>($"The \"email\" argument must not be empty and must consist of no more than {EMAIL_MAX_LENGTH} characters.");

            if(description.Length > DESCRIPTION_MAX_LENGTH)
                return Result.Failure<Volunteer>($"The \"description\" argument must not contain more than {DESCRIPTION_MAX_LENGTH} characters");

            if(phoneNumber.Length != PHONE_NUMBER_LENGTH)
                return Result.Failure<Volunteer>($"The \"phoneNumber\" argument must be {PHONE_NUMBER_LENGTH} characters long.");

            var volunteer = new Volunteer(fullName,
                                          email,
                                          description,
                                          yearsOfExperience,
                                          petsFoundHomeCount,
                                          petsLookingForHomeCount,
                                          petsOnTreatmentCount,
                                          phoneNumber,
                                          socialNetworks,
                                          requisites,
                                          pets);

            return Result.Success(volunteer);
        }

    }
}
