using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Platform.Tenant.Query.GetTenants
{
    public class GetTenantsQuery : PageFilter, IRequest<PagedResult<GetTenantsResponse>>
    {
        public short? IdTenantStatus { get; set; }
        public string? Search { get; set; }
    }
}
