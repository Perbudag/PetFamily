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
            if(string.IsNullOrWhiteSpace(path))
                return Error.Validation("PetPhoto.Create.Invalid", $"The \"path\" argument must not be empty.");

            var photo = new PetPhoto(path, isMain);

            return photo;
        }
    }
}
