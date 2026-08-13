using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PatientsSpec : BaseSpecification<Patient>
    {
        public PatientsSpec()
        {
            AddInclude(p => p.IdentificationType);
        }
    }
}