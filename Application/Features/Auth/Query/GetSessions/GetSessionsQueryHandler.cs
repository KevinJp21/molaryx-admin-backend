using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts.IRepositories;
using Domain.Exceptions;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQueryHandler(
        IUserSessionRepository userSessionRepository,
        ICurrentUser currentUser
    ) : IRequestHandler<GetSessionsQuery, List<GetSessionsResponse>>
    {
        private readonly IUserSessionRepository _userSessionRepository =
            userSessionRepository;

        private readonly ICurrentUser _currentUser =
            currentUser;

        public async Task<List<GetSessionsResponse>> Handle(
            GetSessionsQuery request,
            CancellationToken cancellationToken = default)
        {
            if (_currentUser.IdUser is null)
            {
                throw new NotFoundException(
                    "Usuario no autenticado."
                );
            }

            var sessions =
                await _userSessionRepository.GetActiveSessionsByUserIdAsync(
                    _currentUser.IdUser.Value,
                    cancellationToken
                );

            return [.. sessions.Select(
                session => new GetSessionsResponse
                {
                    IdUserSession = session.IdUserSession,
                    Device = session.Device,
                    IpConnection = session.IpConnection!,
                    CreateAt = session.CreatedAt,
                    ExpireAt = session.ExpiresAt,
                    IsCurrent =
                        session.IdUserSession ==
                        _currentUser.IdUserSession
                }
            )];
        }
    }
}