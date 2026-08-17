using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Treatment.Command.CreateTreatment;
using Application.Features.Treatment.Command.DeleteTreatment;
using Application.Features.Treatment.Command.UpdateTreatment;
using Application.Features.Treatment.Query.GetTreatments;
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

        [Authorize(Policy = PermissionCodes.UPDATE_TREATMENT)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTreatment(
            [FromBody] UpdateTreatmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Tratamiento actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.GET_TREATMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetTreatmentsResponse>>>> GetTreatments(
            [FromQuery] GetTreatmentsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetTreatmentsResponse>>(
                    "Tratamientos obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.DELETE_TREATMENT)]
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTreatment(
            [FromQuery] DeleteTreatmentCommand query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Tratamiento eliminado de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}
