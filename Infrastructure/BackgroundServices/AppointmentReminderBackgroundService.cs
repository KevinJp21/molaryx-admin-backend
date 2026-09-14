using Domain.Contracts.IJobs;

namespace Infrastructure.BackgroundServices
{
    public class AppointmentReminderBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<AppointmentReminderBackgroundService> logger
    ) : BackgroundService
    {
        private static readonly TimeSpan CheckInterval = TimeSpan.FromMinutes(15);

        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<AppointmentReminderBackgroundService> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(CheckInterval);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await RunJobAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
            }
        }

        private async Task RunJobAsync(CancellationToken stoppingToken)
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();

                var job = scope.ServiceProvider
                    .GetRequiredService<IAppointmentReminderJob>();

                var notifiedCount = await job.NotifyUpcomingAppointmentsAsync(stoppingToken);

                _logger.LogInformation(
                    "Recordatorio de citas: se enviaron {NotifiedCount} notificaciones.",
                    notifiedCount);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(
                    ex,
                    "Error al enviar recordatorios de citas.");
            }
        }
    }
}
