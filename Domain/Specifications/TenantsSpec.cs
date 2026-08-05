using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class TenantsSpec : BaseSpecification<Tenant>
    {
        public TenantsSpec()
        {
            AddInclude(t => t.IdentificationType);
            AddInclude(t => t.TenantStatus);
            AddInclude(t => t.TenantType);

            AddInclude(t => t.Users);
            AddInclude($"{nameof(Tenant.Users)}.{nameof(User.IdentificationType)}");
        }
    }
}