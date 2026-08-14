using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PromotionSpec : BaseSpecification<Promotion>
    {
        public static PromotionSpec Available(long idPromotion, DateTime now)
        {
            var spec = new PromotionSpec
            {
                Criteria = p =>
                    p.IdPromotion == idPromotion
                    && p.IsActive
                    && p.StartsAt <= now
                    && (p.EndsAt == null || p.EndsAt > now)
            };
            spec.AddInclude(p => p.PromotionPlans);
            return spec;
        }

        private PromotionSpec()
        {
        }
    }
}
