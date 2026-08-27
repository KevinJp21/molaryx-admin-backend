namespace Domain.Contracts.IServices
{
    public interface INotificationService
    {
        Task NotifyUserAsync(
            long? idTenant,
            long idUser,
            string type,
            string subject,
            string body,
            CancellationToken cancellationToken = default);

        Task<bool> MarkAsReadAsync(long idNotification, CancellationToken cancellationToken = default);

        Task<bool> MarkAllAsReadAsync(CancellationToken cancellationToken = default);
    }
}
