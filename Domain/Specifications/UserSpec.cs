using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class UserSpec : BaseSpecification<User>
    {
        public UserSpec(long idUser)
        {
            Criteria = u => u.IdUser == idUser;
            AddInclude(u => u.UserRole);
            AddInclude(u => u.UserStatus);
            AddInclude(u => u.Tenant);
        }
    }
}
