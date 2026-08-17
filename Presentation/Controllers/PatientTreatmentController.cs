using Application.Common.Mediator.Interfaces;
using Application.Features.PatientTreatment.Command.CreatePatientTreatment;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PatientTreatmentController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.CREATE_PATIENT_TREATMENT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreatePatientTreatment(
            [FromBody] CreatePatientTreatmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Tratamiento del paciente creado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
