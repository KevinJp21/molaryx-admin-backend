using Application.Common.Mediator.Interfaces;
using Application.DTOs.Sessions;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQuery : IRequest<GetSessionsResponseDto>
    {
        public bool? Active { get; set; } = null;
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;
    }
}