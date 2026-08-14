using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class UserSessionsSpec : BaseSpecification<UserSession>
    {
        public UserSessionsSpec(long idUser, bool? active)
        {
            Criteria = us => us.IdUser == idUser;
            OrderByDescending = us => us.CreatedAt;

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

        public static UserSessionsSpec ByRefreshTokenHash(string refreshTokenHash)
        {
            var spec = new UserSessionsSpec
            {
                Criteria = us => us.RefreshTokenHash == refreshTokenHash
            };
            return spec;
        }

        public static UserSessionsSpec ActiveByUser(long idUser)
        {
            return new UserSessionsSpec(idUser, active: true);
        }

        private UserSessionsSpec()
        {
        }
    }
}
