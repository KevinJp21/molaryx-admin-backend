using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Platform.Tenant.Command.ActivateTenant;
using Application.Features.Platform.Tenant.Command.CreateBusinessTenant;
using Application.Features.Platform.Tenant.Query.GetTenants;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers.Platform
{
    [Authorize]
    [Route("api/v1/platform/tenant/[action]")]
    [ApiController]
    public class PlatformTenantsController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_PF_TENANTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetTenantsResponse>>>> GetTenants(
            [FromQuery] GetTenantsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetTenantsResponse>>(
                    "Tenants obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.ACTIVATE_PF_TENANT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> ActivateTenant(
            [FromBody] ActivateTenantCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Cuenta activada de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_PF_BUSINESS_TENANT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateBusinessTenant(
            [FromBody] CreateBusinessTenantCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Cuenta Business creada de manera exitosa",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
