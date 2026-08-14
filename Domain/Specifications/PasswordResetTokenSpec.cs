using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PasswordResetTokenSpec : BaseSpecification<PasswordResetToken>
    {
        public static PasswordResetTokenSpec ByValidToken(string token)
        {
            var spec = new PasswordResetTokenSpec
            {
                Criteria = p =>
                    p.Token == token
                    && p.ExpiresAt > DateTime.UtcNow
                    && p.UsedAt == null
            };
            return spec;
        }

        public static PasswordResetTokenSpec ActiveByUser(long idUser)
        {
            var spec = new PasswordResetTokenSpec
            {
                Criteria = p =>
                    p.IdUser == idUser
                    && p.ExpiresAt > DateTime.UtcNow
                    && p.UsedAt == null
            };
            return spec;
        }

        private PasswordResetTokenSpec()
        {
        }
    }
}
