using Application.Common.Mediator.Interfaces;
using Application.Features.Appointment.Command;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AppointmentController(IMediator _mediator) : ControllerBase
    {

        [Authorize(Policy = PermissionCodes.CREATE_APPOINTMENT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateAppointment(
            [FromBody] CreateAppointmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Cita creada de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
