using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Common.Enity
{
    public class RequisitesForAssistance
    {
        public string Title { get; private set; }
        public string Description { get; private set; }


        private RequisitesForAssistance()
        {

        }
        private RequisitesForAssistance(string title, string description)
        {
            Title = title;
            Description = description;
        }


        public static Result<RequisitesForAssistance> Create(string title, string description)
        {
            var Requisite = new RequisitesForAssistance(title, description);

            return Result.Success(Requisite);
        }
    }
}