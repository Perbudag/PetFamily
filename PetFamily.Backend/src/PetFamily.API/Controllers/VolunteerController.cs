using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Commands.Volunteer.Create;

namespace PetFamily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] ICreateVolunteerHandler createVolunteer,
            [FromBody] CreateVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            var result = await createVolunteer.Handle(command, cancellationToken);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
