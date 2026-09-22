using Domain.Contracts.IServices;

namespace Infrastructure.BackgroundServices
{
    public class ExpiredSubscriptionBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExpiredSubscriptionBackgroundService> logger
    ) : BackgroundService
    {
        private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(12);

        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<ExpiredSubscriptionBackgroundService> _logger = logger;

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            await RunJobAsync(stoppingToken);

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
                await using var scope =
                    _scopeFactory.CreateAsyncScope();

                var subscriptionService =
                    scope.ServiceProvider
                        .GetRequiredService<ITenantSubscriptionService>();

                var updatedCount =
                    await subscriptionService.UpdateExpiredSubscriptionsAsync(
                        stoppingToken
                    );

                _logger.LogInformation(
                    "Se marcaron {UpdatedCount} suscripciones como expiradas.",
                    updatedCount
                );
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(
                    ex,
                    "Error al marcar suscripciones expiradas."
                );
            }
        }
    }
}
