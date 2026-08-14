using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class PromotionService(
        IUnitOfWork unitOfWork
    ) : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Promotion> ValidatePromotionAsync(
            long idPromotion,
            short idPlan,
            CancellationToken cancellationToken
        )
        {
            var promotion = await _unitOfWork.PromotionRepository.GetFirstAsync(
                PromotionSpec.Available(idPromotion, DateTime.UtcNow),
                cancellationToken
            );

            if (promotion is null)
            {
                throw new InvalidOperationException(
                    "La promoción no está disponible."
                );
            }

            var promotionPlan = promotion.PromotionPlans
                .FirstOrDefault(pp => pp.IdPlan == idPlan);

            if (promotionPlan is null)
            {
                throw new InvalidOperationException(
                    "La promoción no está disponible para el plan seleccionado."
                );
            }

            if (promotion.DurationMonths <= 0)
            {
                throw new InvalidOperationException(
                    "La configuración de la promoción no es válida."
                );
            }

            return promotion;
        }

        public decimal GetPromotionPrice(
            Promotion promotion,
            short idPlan
        )
        {
            var promotionPlan = promotion.PromotionPlans
                .FirstOrDefault(x => x.IdPlan == idPlan);

            if (promotionPlan is null)
            {
                throw new InvalidOperationException(
                    "La promoción no está disponible para el plan seleccionado."
                );
            }

            return promotionPlan.Price;
        }

        public DateTime CalculatePromotionEndDate(
            Promotion promotion,
            DateTime startsAt
        )
        {
            if (promotion.DurationMonths <= 0)
            {
                throw new InvalidOperationException(
                    "La promoción no tiene una duración válida."
                );
            }

            return startsAt.AddMonths(promotion.DurationMonths);
        }
    }
}