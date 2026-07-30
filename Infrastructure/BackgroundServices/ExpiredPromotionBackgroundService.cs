using Domain.Contracts.IServices;

namespace Infrastructure.BackgroundServices
{
    public class ExpiredPromotionBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExpiredPromotionBackgroundService> logger
    ) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<ExpiredPromotionBackgroundService> _logger = logger;

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
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
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error al actualizar promociones expiradas."
                    );
                }

                await Task.Delay(
                    TimeSpan.FromDays(1),
                    stoppingToken
                );
            }
        }
    }
}