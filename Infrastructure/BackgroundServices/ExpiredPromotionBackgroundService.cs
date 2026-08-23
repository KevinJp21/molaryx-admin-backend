using Domain.Contracts.IServices;

namespace Infrastructure.BackgroundServices
{
    public class ExpiredPromotionBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExpiredPromotionBackgroundService> logger
    ) : BackgroundService
    {
        private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(12);

        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<ExpiredPromotionBackgroundService> _logger = logger;

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(CheckInterval);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    try
                    {
                        await using var scope =
                            _scopeFactory.CreateAsyncScope();

                        var subscriptionService =
                            scope.ServiceProvider
                                .GetRequiredService<ITenantSubscriptionService>();

                        var updatedCount =
                            await subscriptionService.UpdateExpiredPromotionsAsync(
                                stoppingToken
                            );

                        _logger.LogInformation(
                            "Se actualizaron {UpdatedCount} suscripciones con promociones expiradas.",
                            updatedCount
                        );
                    }
                    catch (Exception ex) when (ex is not OperationCanceledException)
                    {
                        _logger.LogError(
                            ex,
                            "Error al actualizar promociones expiradas."
                        );
                    }
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
            }
        }
    }
}
