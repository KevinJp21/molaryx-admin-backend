using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Procedure.Command.CreateProcedure;
using Application.Features.Procedure.Command.DeleteProcedure;
using Application.Features.Procedure.Command.UpdateProcedure;
using Application.Features.Procedure.Query;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ProcedureController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_PROCEDURES)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetProceduresResponse>>>> GetProcedures(
            [FromQuery] GetProceduresQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetProceduresResponse>>(
                    "Procedimientos obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_PROCEDURE)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateProcedure(
            [FromBody] CreateProcedureCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Procedimiento creado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.UPDATE_PROCEDURE)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProcedure(
            [FromBody] UpdateProcedureCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Procedimiento actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.DELETE_PROCEDURE)]
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProcedure(
            [FromQuery] DeleteProcedureCommand query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Procedimiento eliminado de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}
