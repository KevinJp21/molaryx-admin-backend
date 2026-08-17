using Application.Common.Mediator.Interfaces;
using Application.Features.Treatment.Command.CreateTreatment;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class TreatmentController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.CREATE_TREATMENT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateTreatment(
            [FromBody] CreateTreatmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Tratamiento creado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
