using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.DTOs.Sessions;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQuery : PageFilter, IRequest<PagedResult<UserSessionDto>>
    {
        public bool? Active { get; set; } = null;
    }
}