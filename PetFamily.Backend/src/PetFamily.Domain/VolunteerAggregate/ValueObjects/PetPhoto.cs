using CSharpFunctionalExtensions;

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
                return Result.Failure<PetPhoto>($"The \"path\" argument must not be empty.");

            var photo = new PetPhoto(path, isMain);

            return Result.Success(photo);
        }
    }
}
