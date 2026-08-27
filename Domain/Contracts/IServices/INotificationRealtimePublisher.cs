using Domain.Models.Notifications;

namespace Domain.Contracts.IServices
{
    public interface INotificationRealtimePublisher
    {
        Task PublishToUserAsync(
            long idUser,
            NotificationRealtimePayload payload,
            CancellationToken cancellationToken = default);
    }
}
