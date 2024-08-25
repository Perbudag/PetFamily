using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class SocialNetworks
    {
        public string Name { get; private set; }
        public string Path { get; private set; }

        private SocialNetworks()
        {
            
        }
        private SocialNetworks(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public static Result<SocialNetworks> Create(string name, string path)
        {
            var network = new SocialNetworks(name, path);

            return Result.Success(network);
        }
    }
}