using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace PetFamily.Domain.Pet
{
    public class Pet
    {
        private List<Requisite> _Requisites;

        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string HealthInformation { get; set; }
        private string ResidentialAddress { get; set; }
        private float Weight { get; set; }
        private float Height { get; set; }
        private string PhoneNumber { get; set; }
        private DateTime DateOfBirth { get; set; }
        private bool IsCastrated { get; set; }
        private bool IsVaccinated { get; set; }
        private AssistanceStatus AssistanceStatus { get; set; }
        private IReadOnlyList<Requisite> Requisites => _Requisites.AsReadOnly();
        private DateTime CreatedAt { get; set; }

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
