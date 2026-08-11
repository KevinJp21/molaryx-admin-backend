using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.DTOs.Tenant;

namespace Application.Features.Platform.Tenant.Query.GetTenants
{
    public class GetTenantsQuery : PageFilter, IRequest<PagedResult<TenantDto>> { }
}