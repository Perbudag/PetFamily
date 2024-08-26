using CSharpFunctionalExtensions;
using PetFamily.Domain.Common.ValueObjects;
using PetFamily.Domain.PetAggregate;
using System.Collections.Generic;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class Volunteer
    {
        private List<SocialNetwork> _SocialNetworks;
        private List<RequisitesForAssistance> _Requisites;
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
        public IReadOnlyList<RequisitesForAssistance> Requisites => _Requisites.AsReadOnly();
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
                          List<RequisitesForAssistance> requisites,
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

        public static Result<Volunteer> Create(string firstname,
                                               string lastname,
                                               string patronymic,
                                               string email,
                                               string description,
                                               int yearsOfExperience,
                                               int petsFoundHomeCount,
                                               int petsLookingForHomeCount,
                                               int petsOnTreatmentCount,
                                               string phoneNumber,
                                               List<SocialNetwork> socialNetworks,
                                               List<RequisitesForAssistance> requisites,
                                               List<Pet> pets)
        {
            var volunteer = new Volunteer(new FullName(firstname, lastname, patronymic),
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
