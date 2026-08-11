using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Users.Patient.Query
{
    public class GetPatientsQuery : PageFilter, IRequest<PagedResult<GetPatientsResponse>>
    {}
}