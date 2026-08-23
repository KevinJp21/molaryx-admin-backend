using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Procedure.Query
{
    public class GetProceduresQuery : PageFilter, IRequest<PagedResult<GetProceduresResponse>>
    {
        public bool? IsActive { get; set; }
        public string? Search { get; set; }
    }
}
