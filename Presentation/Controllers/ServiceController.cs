using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Service.Command.CreateService;
using Application.Features.Service.Command.DeleteService;
using Application.Features.Service.Command.UpdateService;
using Application.Features.Service.Query;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ServiceController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_SERVICES)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetServicesResponse>>>> GetServices(
            [FromQuery] GetServicesQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetServicesResponse>>(
                    "Servicios obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_SERVICE)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateService(
            [FromBody] CreateServiceCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Servicio creado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.UPDATE_SERVICE)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateService(
            [FromBody] UpdateServiceCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Servicio actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.DELETE_SERVICE)]
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteService(
            [FromQuery] DeleteServiceCommand query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Servicio eliminado de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}
