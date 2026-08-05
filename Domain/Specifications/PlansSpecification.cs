using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PublicPlansSpecification : BaseSpecification<Plan>
    {
        public PublicPlansSpecification()
        {
            Criteria = p => p.IsActive == true;
            AddInclude(p => p.PromotionPlans);
            AddInclude($"{nameof(Plan.PromotionPlans)}.{nameof(PromotionPlan.Promotion)}");
        }
    }
}
