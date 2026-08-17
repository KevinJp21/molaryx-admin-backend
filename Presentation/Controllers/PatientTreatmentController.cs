using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.PatientTreatment.Command.CreatePatientTreatment;
using Application.Features.PatientTreatment.Command.UpdatePatientTreatment;
using Application.Features.PatientTreatment.Query.GetPatientTreatments;
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
        [Authorize(Policy = PermissionCodes.GET_PATIENT_TREATMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetPatientTreatmentsResponse>>>> GetPatientTreatments(
            [FromQuery] GetPatientTreatmentsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetPatientTreatmentsResponse>>(
                    "Tratamientos del paciente obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

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

        [Authorize(Policy = PermissionCodes.UPDATE_PATIENT_TREATMENT)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePatientTreatment(
            [FromBody] UpdatePatientTreatmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Tratamiento del paciente actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
