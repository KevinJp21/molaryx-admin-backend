using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ProfessionalSpec : BaseSpecification<Professional>
    {
        public ProfessionalSpec(long idTenant, short? idUserStatus = null)
        {
            Criteria = p => p.IdTenant == idTenant;
            AddListIncludes();
            OrderBy = p => p.User.FirstName;

            if (idUserStatus != null)
            {
                Criteria = And(p => p.User.IdUserStatus == idUserStatus);
            }
        }

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

        private void AddListIncludes()
        {
            AddInclude(p => p.User);
            AddInclude($"{nameof(Professional.User)}.{nameof(User.UserStatus)}");
            AddInclude($"{nameof(Professional.User)}.{nameof(User.IdentificationType)}");
        }
    }
}
