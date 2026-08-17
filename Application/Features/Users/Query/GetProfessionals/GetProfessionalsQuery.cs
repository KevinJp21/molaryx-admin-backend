using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Users.Query.GetProfessionals
{
    public class GetProfessionalsQuery : PageFilter, IRequest<PagedResult<GetProfessionalsResponse>>
    {
        public short? IdUserStatus { get; set; } = null;
        public string? Search { get; set; }
    }
}