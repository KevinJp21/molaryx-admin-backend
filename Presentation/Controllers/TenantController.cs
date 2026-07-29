using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.DTOs.Tenant;
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

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<TenantDto>>>> GetTenants ([FromQuery] GetTenantsQuery query, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<TenantDto>>(
                    "Tenants obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}