using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Notification.Query.GetNotifications
{
    public class GetNotificationsQuery : PageFilter, IRequest<PagedResult<GetNotificationsResponse>>
    {
        public bool OnlyUnviewed { get; set; }
    }
}
