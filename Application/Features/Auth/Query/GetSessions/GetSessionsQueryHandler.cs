using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Context;
using Application.DTOs.Sessions;
using Domain.Contracts.IRepositories;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQueryHandler(
        IUserSessionRepository userSessionRepository,
        ICurrentUser currentUser
    ) : IRequestHandler<GetSessionsQuery, GetSessionsResponseDto>
    {
        private readonly IUserSessionRepository _userSessionRepository =
            userSessionRepository;

        private readonly ICurrentUser _currentUser =
            currentUser;

        public async Task<GetSessionsResponseDto> Handle(
            GetSessionsQuery request,
            CancellationToken cancellationToken = default)
        {
            var page = request.Page is > 0
                ? request.Page.Value
                : PaginationDefaults.DefaultPage;

            var pageSize = request.Size is > 0
                ? Math.Min(
                    request.Size.Value,
                    PaginationDefaults.MaxSize
                )
                : PaginationDefaults.DefaultSize;

            var (totalItems, sessions) =
                await _userSessionRepository.GetAllSessionsByUserIdAsync(
                    _currentUser.IdUser!.Value,
                    request.Active,
                    page,
                    pageSize,
                    cancellationToken
                );

            return new GetSessionsResponseDto
            {
                Items =
                [
                    .. sessions.Select(
                session => new UserSessionDto
                {
                    IdUserSession = session.IdUserSession,
                    Device = session.Device!,
                    IpConnection = session.IpConnection ?? string.Empty,
                    CreatedAt = session.CreatedAt,
                    ExpiresAt = session.ExpiresAt,
                    IsCurrent =
                        session.IdUserSession ==
                        _currentUser.IdUserSession
                }
            )
                ],
                Page = page,
                Size = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)pageSize
                )
            };
        }
    }
}