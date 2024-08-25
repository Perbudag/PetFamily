using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace PetFamily.Domain.Pet
{
    public class Pet
    {
        private List<Requisite> _Requisites;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HealthInformation { get; set; }
        public string ResidentialAddress { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsCastrated { get; set; }
        public bool IsVaccinated { get; set; }
        public AssistanceStatus AssistanceStatus { get; set; }
        public IReadOnlyList<Requisite> Requisites => _Requisites.AsReadOnly();
        public DateTime CreatedAt { get; set; }

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
