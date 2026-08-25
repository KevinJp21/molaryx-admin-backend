using Domain.Contracts.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs
{
    [Authorize]
    public class NotificationHub(INotificationService _notificationService) : Hub
    {
        [HubMethodName("mark_as_viewed")]
        public Task<bool> MarkAsViewed(long notificationId)
            => _notificationService.MarkAsReadAsync(notificationId, Context.ConnectionAborted);

        [HubMethodName("mark_all_as_viewed")]
        public Task<bool> MarkAllAsViewed()
            => _notificationService.MarkAllAsReadAsync(Context.ConnectionAborted);
    }
}
