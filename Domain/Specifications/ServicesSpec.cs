using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ServicesSpec : BaseSpecification<Service>
    {
        public ServicesSpec(long idTenant)
        {
            Criteria = p => p.IdTenant == idTenant && p.DeletedAt == null;
            OrderByDescending = p => p.CreatedAt;
        }

        public static ServicesSpec ById(long idService)
        {
            var spec = new ServicesSpec
            {
                Criteria = p => p.IdService == idService && p.DeletedAt == null
            };
            return spec;
        }

        private ServicesSpec()
        {
        }
    }
}
