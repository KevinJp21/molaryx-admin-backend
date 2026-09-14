namespace Domain.Contracts.IServices
{
    public interface INotificationHandler
    {
        Task NotifyAppointmentAssignedAsync(
            long idTenant,
            long idUser,
            long idAppointment,
            CancellationToken cancellationToken = default);

        Task NotifyAppointmentReminderAsync(
            long idTenant,
            IReadOnlyCollection<long> idUsers,
            long idAppointment,
            DateTime startAt,
            string patientName,
            CancellationToken cancellationToken = default);

        Task NotifyTenantRegisteredAsync(
            long idTenant,
            string consultoryName,
            CancellationToken cancellationToken = default);
    }
}
