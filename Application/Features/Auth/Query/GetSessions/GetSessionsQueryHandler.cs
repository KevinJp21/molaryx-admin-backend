using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Context;
using Application.DTOs.Sessions;
using Domain.Contracts;
using Domain.Specifications;

namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser
    ) : IRequestHandler<GetSessionsQuery, PagedResult<UserSessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly ICurrentUser _currentUser =
            currentUser;

        public async Task<PagedResult<UserSessionDto>> Handle(
            GetSessionsQuery request,
            CancellationToken cancellationToken = default)
        {
            var spec = new UserSessionsSpec(
                _currentUser.IdUser!.Value,
                request.Active
            );

            var (totalItems, sessions) = await _unitOfWork.UserSessionRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<UserSessionDto>
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
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size)
                )
            };
        }
    }
}