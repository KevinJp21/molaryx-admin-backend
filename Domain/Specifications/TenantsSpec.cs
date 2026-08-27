using Domain.Common;
using Domain.Common.Tenants;
using Domain.Entities;

namespace Domain.Specifications
{
    public class TenantsSpec : BaseSpecification<Tenant>
    {
        public TenantsSpec(short? idTenantStatus = null, string? search = null)
        {
            OrderByDescending = t => t.CreatedAt;

            AddInclude(t => t.IdentificationType);
            AddInclude(t => t.TenantStatus);
            AddInclude(t => t.TenantType);

            AddInclude(t => t.Users);
            AddInclude($"{nameof(Tenant.Users)}.{nameof(User.IdentificationType)}");
            AddInclude(t => t.TenantSubscriptions);
            AddInclude($"{nameof(Tenant.TenantSubscriptions)}.{nameof(TenantSubscription.Plan)}");
            AddInclude(
                $"{nameof(Tenant.TenantSubscriptions)}.{nameof(TenantSubscription.TenantSubscriptionStatus)}");

            if (idTenantStatus.HasValue)
            {
                Criteria = And(t => t.IdTenantStatus == idTenantStatus.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var tokens = TenantSearch.GetTokens(search);

                foreach (var token in tokens)
                {
                    Criteria = And(TenantSearch.MatchesToken(token));
                }
            }
        }
    }
}
