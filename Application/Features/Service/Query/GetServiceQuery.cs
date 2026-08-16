using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Service.Query
{
    public class GetServicesQuery : PageFilter, IRequest<PagedResult<GetServicesResponse>>
    {
        public bool? IsActive { get; set; }
    }
}
