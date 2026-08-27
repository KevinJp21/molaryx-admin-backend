namespace Domain.Contracts.IJobs
{
    public interface IAppointmentReminderJob
    {
        Task<int> NotifyUpcomingAppointmentsAsync(CancellationToken cancellationToken = default);
    }
}
