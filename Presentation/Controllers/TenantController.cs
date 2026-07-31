using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.DTOs.Tenant;
using Application.Features.Tenant.Command.ActivateTenant;
using Application.Features.Tenant.Command.CreateBusinessTenant;
using Application.Features.Tenant.Query.GetTenants;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class TenantController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Policy = PermissionCodes.GET_TENANTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<TenantDto>>>> GetTenants([FromQuery] GetTenantsQuery query, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<TenantDto>>(
                    "Tenants obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.ACTIVATE_TENANT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> ActivateTenant([FromBody] ActivateTenantCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Cuenta activada de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_BUSINESS_TENANT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateBusinessTenant([FromBody] CreateBusinessTenantCommand body, CancellationToken cancellationToken)
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