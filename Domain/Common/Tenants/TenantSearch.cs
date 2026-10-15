using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Common.Tenants
{
    public static class TenantSearch
    {
        public static Expression<Func<Tenant, bool>> MatchesToken(string token)
        {
            var normalized = token.ToLowerInvariant();

            return Search.ClassifyToken(token) switch
            {
                SearchTermKind.Email =>
                    tenant =>
                        tenant.Email.ToLower().Contains(normalized) ||
                        tenant.Users.Any(u =>
                            u.DeletedAt == null &&
                            u.Email.ToLower().Contains(normalized)),

                SearchTermKind.Numeric =>
                    tenant =>
                        tenant.PhoneNumber.Contains(token) ||
                        (tenant.IdentificationNumber != null &&
                         tenant.IdentificationNumber.ToLower().Contains(normalized)) ||
                        tenant.Users.Any(u =>
                            u.DeletedAt == null &&
                            (u.PhoneNumber.Contains(token) ||
                             u.IdentificationNumber.ToLower().Contains(normalized))),

                SearchTermKind.Identification =>
                    tenant =>
                        (tenant.IdentificationNumber != null &&
                         tenant.IdentificationNumber.ToLower().Contains(normalized)) ||
                        tenant.Users.Any(u =>
                            u.DeletedAt == null &&
                            u.IdentificationNumber.ToLower().Contains(normalized)),

                _ => tenant =>
                    tenant.ConsultoryName.ToLower().Contains(normalized) ||
                    tenant.Users.Any(u =>
                        u.DeletedAt == null &&
                        (u.Username.ToLower().Contains(normalized) ||
                         u.FirstName.ToLower().Contains(normalized) ||
                         (u.SecondName != null && u.SecondName.ToLower().Contains(normalized)) ||
                         u.FirstSurname.ToLower().Contains(normalized) ||
                         (u.SecondSurname != null && u.SecondSurname.ToLower().Contains(normalized))))
            };
        }
    }
}
