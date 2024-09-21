using PetFamily.Application.Commands.Volunteer.UpdateRequisites;
using PetFamily.Application.Dto;

namespace PetFamily.API.Contracts.Volunteer
{
    public record VolunteerUpdateRequisitesRequest(IEnumerable<RequisiteForAssistanceDto> Requisites)
    {
        public VolunteerUpdateRequisitesCommand ToCommand(Guid id) =>
            new VolunteerUpdateRequisitesCommand(id, Requisites);
    }
}
