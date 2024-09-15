using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record PetPhoto
    {
        public string Path { get; }
        public bool IsMain { get; }

        public PetPhoto(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }

        public static Result<PetPhoto> Create(string path, bool isMain = false)
        {
            List<Error> errors = [];

            if(string.IsNullOrWhiteSpace(path))
                errors.Add(Errors.General.Validation.NotBeEmpty("petPhoto"));

            if (errors.Count > 0)
                return errors;

            var photo = new PetPhoto(path, isMain);

            return photo;
        }
    }
}
