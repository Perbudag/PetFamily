using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Contracts.Volunteer;
using PetFamily.API.Extensions;
using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Commands.Volunteer.Update;
using PetFamily.Application.Commands.Volunteer.UpdateRequisites;
using PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks;

namespace PetFamily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] ICreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);

            return result.ToResponse(StatusCodes.Status201Created);
        }


        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromServices] IUpdateVolunteerMainInfoHandler handler,
            [FromRoute]Guid id,
            [FromBody] UpdateVolunteerMainInfoRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            return result.ToResponse();
        }


        [HttpPut("{id:guid}/social-networks")]
        public async Task<ActionResult<Guid>> UpdateSocialNetworks(
            [FromServices] IUpdateSocialNetworksHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateSocialNetworksRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            return result.ToResponse();
        }


        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromServices] IUpdateRequisitesHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateRequisitesRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            return result.ToResponse();
        }
    }
}
