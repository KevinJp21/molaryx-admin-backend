using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ProceduresSpec : BaseSpecification<Procedure>
    {
        public ProceduresSpec(long idTenant, bool? isActive, string? search = null)
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
                    Criteria = And(p =>
                        p.Name.ToLower().Contains(token) ||
                        (p.Description != null && p.Description.ToLower().Contains(token)));
                }
            }
        }

        public static ProceduresSpec ById(long idProcedure)
        {
            return new ProceduresSpec
            {
                Criteria = p => p.IdProcedure == idProcedure && p.DeletedAt == null
            };
        }

        public static ProceduresSpec ByName(long idTenant, string name)
        {
            return new ProceduresSpec
            {
                Criteria = p => p.IdTenant == idTenant
                    && p.DeletedAt == null
                    && p.Name.ToLower() == name.ToLower()
            };
        }

        private ProceduresSpec()
        {
        }
    }
}
