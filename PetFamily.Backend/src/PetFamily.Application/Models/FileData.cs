namespace PetFamily.Application.Models
{
    public record FileData(Stream? Stream, FileLocation Location);

    public record FileLocation(string BucketName, string FileName);
}
