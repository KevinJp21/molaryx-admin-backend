using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class UserSessionsSpec : BaseSpecification<UserSession>
    {
        public UserSessionsSpec(long idUser, bool? active)
        {
            Criteria = us => us.IdUser == idUser;

            if (active == true)
            {
                Criteria = And(us =>
                    us.RevokedAt == null &&
                    us.ExpiresAt > DateTime.UtcNow);
            }
            else if (active == false)
            {
                Criteria = And(us =>
                    us.RevokedAt != null ||
                    us.ExpiresAt <= DateTime.UtcNow);
            }
        }
    }
}
