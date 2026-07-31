using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface IPromotionService
    {
        Task<Promotion> ValidatePromotionAsync(
            long idPromotion,
            short idPlan,
            CancellationToken cancellationToken
        );

        decimal GetPromotionPrice(
            Promotion promotion,
            short idPlan
        );

        DateTime CalculatePromotionEndDate(
            Promotion promotion,
            DateTime startsAt
        );
    }
}