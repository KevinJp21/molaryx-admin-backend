using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQuery : IRequest<List<GetSessionsResponse>> { }
}