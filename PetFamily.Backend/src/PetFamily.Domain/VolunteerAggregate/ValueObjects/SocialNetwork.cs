using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record SocialNetwork
    {
        public const int NAME_MAX_LENGTH = 100;

        public string Name { get; }
        public string Path { get; }

        private SocialNetwork(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public static Result<SocialNetwork> Create(string name, string path)
        {
            if(string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                return Result.Failure<SocialNetwork>($"The \"name\" argument must not be empty and must consist of no more than {NAME_MAX_LENGTH} characters.");

            if(string.IsNullOrWhiteSpace(path))
                return Result.Failure<SocialNetwork>($"The \"path\" argument must not be empty.");

            var network = new SocialNetwork(name, path);

            return Result.Success(network);
        }
    }
}