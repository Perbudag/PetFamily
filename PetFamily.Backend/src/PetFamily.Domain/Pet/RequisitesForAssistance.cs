using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pet
{
    public class Requisite
    {
        private Requisite()
        {
            
        }
        private Requisite(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public static Result<Requisite> Create(string title, string description)
        {
            var Requisite = new Requisite(title, description);

            return Result.Success(Requisite);
        }
    }
}