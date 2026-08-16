using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Patients.Query
{
    public class GetPatientsQuery : PageFilter, IRequest<PagedResult<GetPatientsResponse>>
    {
        public bool? IsActive { get; set; } = null;
    }
}
