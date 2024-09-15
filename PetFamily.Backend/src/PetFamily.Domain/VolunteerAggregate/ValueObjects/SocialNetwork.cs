using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record SocialNetwork
    {
        public const int NAME_MAX_LENGTH = 100;
        public const int PATH_MAX_LENGTH = 300;

        public string Name { get; }
        public string Path { get; }

        private SocialNetwork(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public static Result<SocialNetwork> Create(string name, string path)
        {
            List<Error> errors = [];

            if(string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("socialNetwork.name", NAME_MAX_LENGTH));

            if(string.IsNullOrWhiteSpace(path) || path.Length > PATH_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("socialNetwork.path", PATH_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;

            var network = new SocialNetwork(name, path);

            return network;
        }
    }
}