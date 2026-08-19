using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.ClinicalRecord.Command.CreateClinicalRecord;
using Application.Features.ClinicalRecord.Query.GetClinicalRecords;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ClinicalRecordController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_CLINICAL_RECORDS)]
        [HttpGet]
        [EndpointDescription("Obtiene los registros clínicos de un paciente.")]
        public async Task<ActionResult<ApiResponse<PagedResult<GetClinicalRecordsResponse>>>> GetClinicalRecords(
            [FromQuery] GetClinicalRecordsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetClinicalRecordsResponse>>(
                    "Registros clínicos obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_CLINICAL_RECORD)]
        [HttpPost]
        [EndpointDescription("Crea un nuevo registro clínico para un paciente.")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateClinicalRecord([FromBody] CreateClinicalRecordCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Registro clínico creado exitosamente.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
