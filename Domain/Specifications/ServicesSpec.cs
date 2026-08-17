using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ServicesSpec : BaseSpecification<Service>
    {
        public ServicesSpec(long idTenant, bool? isActive, string? search = null)
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

            if (!string.IsNullOrWhiteSpace(search))
            {
                foreach (var token in SearchText.Tokens(search))
                {
                    Criteria = And(s =>
                        s.Name.ToLower().Contains(token) ||
                        (s.Description != null && s.Description.ToLower().Contains(token)));
                }
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
