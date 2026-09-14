using Domain.Contracts.IServices;
using Domain.Models.Notifications;
using Microsoft.AspNetCore.SignalR;
using Presentation.Hubs;

namespace Infrastructure.Services
{
    public class NotificationRealtimePublisher(
        IHubContext<NotificationHub> _hubContext
    ) : INotificationRealtimePublisher
    {
        public const string ClientEventName = "client_new_notification";

        public Task PublishToUserAsync(
            long idUser,
            NotificationRealtimePayload payload,
            CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients
                .User(idUser.ToString())
                .SendAsync(ClientEventName, payload, cancellationToken);
        }
    }
}
