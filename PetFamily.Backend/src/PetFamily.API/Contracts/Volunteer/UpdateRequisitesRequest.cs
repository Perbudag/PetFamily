using PetFamily.Application.Commands.Volunteer.UpdateRequisites;
using PetFamily.Application.Dto;

namespace PetFamily.API.Contracts.Volunteer
{
    public record UpdateRequisitesRequest(IEnumerable<RequisiteForAssistanceDto> Requisites)
    {
        public UpdateRequisitesCommand ToCommand(Guid id) =>
            new UpdateRequisitesCommand(id, Requisites);
    }
}
