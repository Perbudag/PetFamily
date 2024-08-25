using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace PetFamily.Domain.Pet
{
    public class Pet
    {
        private List<Requisite> _Requisites;

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
        public IReadOnlyList<Requisite> Requisites => _Requisites.AsReadOnly();
        public DateTime CreatedAt { get; private set; }

        private Pet()
        {

        }

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
                   List<Requisite> requisites)
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
                                  List<Requisite> requisites)
        {
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
                              requisites);

            return Result.Success(pet);
        }
    }
}
