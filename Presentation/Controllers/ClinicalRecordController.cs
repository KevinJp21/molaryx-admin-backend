using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.ClinicalRecord.Command.CreateClinicalRecord;
using Application.Features.ClinicalRecord.Query.GetClinicalHistory;
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

        [Authorize(Policy = PermissionCodes.GET_CLINICAL_RECORDS)]
        [HttpGet]
        [Produces("application/pdf")]
        public async Task<IActionResult> GetClinicalHistory(
            [FromQuery] GetClinicalHistoryQuery query,
            CancellationToken cancellationToken)
        {
            var (content, fileName) = await _mediator.Send(query, cancellationToken);

            return File(content, "application/pdf", fileName);
        }

        [Authorize(Policy = PermissionCodes.CREATE_CLINICAL_RECORD)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateClinicalRecord([FromBody] CreateClinicalRecordCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Registro clínico creado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
