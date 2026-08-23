using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Users.Query.GetTeam
{
    public class GetTeamQuery : PageFilter, IRequest<PagedResult<GetTeamResponse>>
    {
        public short? IdUserStatus { get; set; }
        public List<short>? IdUserRoles { get; set; }
        public string? Search { get; set; }
    }
}
