using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Context;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Enums;
using Domain.Specifications;

namespace Application.Features.Notification.Query.GetNotifications
{
    public class GetNotificationsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ICurrentUser _currentUser
    ) : IRequestHandler<GetNotificationsQuery, PagedResult<GetNotificationsResponse>>
    {
        public async Task<PagedResult<GetNotificationsResponse>> Handle(
            GetNotificationsQuery request,
            CancellationToken cancellationToken = default)
        {
            var idUser = _currentUser.IdUser!.Value;
            long? idTenant = null;

            if (_currentUser.IdUserRole != (short)UserRoleEnum.SUPER_ADMIN)
            {
                var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
                idTenant = access.IdTenant;
            }

            var page = PaginationHelper.GetEffectivePage(request.Page);
            var size = PaginationHelper.GetEffectivePageSize(request.Size);

            var spec = new NotificationsSpec(idTenant, idUser, request.OnlyUnviewed);

            var (totalItems, notifications) = await _unitOfWork.NotificationRepository.GetPagedAsync(
                page,
                size,
                spec,
                cancellationToken);

            return new PagedResult<GetNotificationsResponse>
            {
                Items =
                [
                    .. notifications.Select(n => new GetNotificationsResponse
                    {
                        IdNotification = n.IdNotification,
                        Type = n.Type,
                        Subject = n.Subject,
                        Body = n.Body,
                        IsViewed = n.IsViewed,
                        CreatedAt = n.CreatedAt
                    })
                ],
                Page = page,
                Size = size,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }
    }
}
