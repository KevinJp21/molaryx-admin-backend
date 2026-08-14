using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Patients.Command.CreatePatient;
using Application.Features.Patients.Command.UpdatePatient;
using Application.Features.Patients.Query;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PatientController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_PATIENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetPatientsResponse>>>> GetPatients(
            [FromQuery] GetPatientsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetPatientsResponse>>(
                    "Pacientes obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_PATIENT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreatePatient(
            [FromBody] CreatePatientCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Paciente creado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.UPDATE_PATIENT)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePatient(
            [FromBody] UpdatePatientCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Paciente actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
