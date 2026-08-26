using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Specifications
{
    public class TenantSubscriptionSpec : BaseSpecification<TenantSubscription>
    {
        public static TenantSubscriptionSpec ActiveByTenant(long idTenant)
        {
            var spec = new TenantSubscriptionSpec
            {
                Criteria = ts =>
                    ts.IdTenant == idTenant
                    && ts.IdTenantSubscriptionStatus == (short)TenantSubscriptionStatusEnum.ACTIVE
            };
            spec.AddInclude(ts => ts.TenantSubscriptionStatus);
            spec.AddInclude(ts => ts.Plan);
            return spec;
        }

        public static TenantSubscriptionSpec LatestByTenant(long idTenant)
        {
            var spec = new TenantSubscriptionSpec
            {
                Criteria = ts => ts.IdTenant == idTenant,
                OrderByDescending = ts => ts.StartsAt
            };
            spec.AddInclude(ts => ts.TenantSubscriptionStatus);
            spec.AddInclude(ts => ts.Plan);
            return spec;
        }

        public static TenantSubscriptionSpec ByIdWithPromotion(long idTenantSubscription)
        {
            var spec = new TenantSubscriptionSpec
            {
                Criteria = ts => ts.IdTenantSubscription == idTenantSubscription
            };
            spec.AddInclude(nameof(TenantSubscription.Promotion));
            return spec;
        }

        public static TenantSubscriptionSpec WithExpiredPromotions(DateTime currentDate)
        {
            var spec = new TenantSubscriptionSpec
            {
                Criteria = ts =>
                    ts.IdTenantSubscriptionStatus == (short)TenantSubscriptionStatusEnum.ACTIVE
                    && ts.IdPromotion.HasValue
                    && ts.PromotionEndsAt.HasValue
                    && ts.PromotionEndsAt.Value <= currentDate
            };
            spec.AddInclude(nameof(TenantSubscription.Promotion));
            spec.AddInclude(ts => ts.Plan);
            return spec;
        }

        public static TenantSubscriptionSpec WithExpiredSubscriptions(DateTime currentDate)
        {
            return new TenantSubscriptionSpec
            {
                Criteria = ts =>
                    ts.IdTenantSubscriptionStatus == (short)TenantSubscriptionStatusEnum.ACTIVE
                    && ts.EndsAt.HasValue
                    && ts.EndsAt.Value <= currentDate
            };
        }

        private TenantSubscriptionSpec()
        {
        }
    }
}
