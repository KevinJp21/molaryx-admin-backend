using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Treatment.Query.GetTreatments
{
    public class GetTreatmentsQuery : PageFilter, IRequest<PagedResult<GetTreatmentsResponse>> {
        public bool? IsActive { get; set; }
    }
}
