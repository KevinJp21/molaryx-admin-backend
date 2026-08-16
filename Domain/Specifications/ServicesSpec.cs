using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ServicesSpec : BaseSpecification<Service>
    {
        public ServicesSpec(long idTenant, bool? isActive)
        {
            Criteria = p => p.IdTenant == idTenant && p.DeletedAt == null;
            OrderByDescending = p => p.CreatedAt;

            if (isActive == true)
            {
                Criteria = And(p => p.IsActive == true);
            }
            else if (isActive == false)
            {
                Criteria = And(p => p.IsActive == false);
            }
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
