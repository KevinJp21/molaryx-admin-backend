using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ProfessionalSpec : BaseSpecification<Professional>
    {
        public static ProfessionalSpec ByUser(long idTenant, long idUser)
        {
            var spec = new ProfessionalSpec
            {
                Criteria = p => p.IdTenant == idTenant && p.IdUser == idUser
            };
            return spec;
        }

        private ProfessionalSpec()
        {
        }
    }
}
