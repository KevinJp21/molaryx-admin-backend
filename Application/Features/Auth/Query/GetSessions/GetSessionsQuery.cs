using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQuery : IRequest<GetSessionsResponse>
    {
        public bool? Active { get; set; } = null;
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;
    }
}