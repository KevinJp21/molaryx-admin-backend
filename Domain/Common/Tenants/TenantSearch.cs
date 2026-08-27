using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Common.Tenants
{
    public static class TenantSearch
    {
        public const int MinTokenLength = 3;

        public static string[] GetTokens(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return [];
            }

            return [.. SearchText.Tokens(search)
                .Where(token => token.Length >= MinTokenLength)];
        }

        public static bool HasValidSearch(string? search)
            => GetTokens(search).Length > 0;

        public static TenantSearchTermKind ClassifyToken(string token)
        {
            if (token.Contains('@'))
            {
                return TenantSearchTermKind.Email;
            }

            if (token.All(char.IsDigit))
            {
                return TenantSearchTermKind.Numeric;
            }

            if (token.Any(char.IsDigit) && token.Any(char.IsLetter))
            {
                return TenantSearchTermKind.Identification;
            }

            return TenantSearchTermKind.Name;
        }

        public static Expression<Func<Tenant, bool>> MatchesToken(string token)
        {
            var normalized = token.ToLowerInvariant();

            return ClassifyToken(token) switch
            {
                TenantSearchTermKind.Email =>
                    tenant =>
                        tenant.Email.ToLower().Contains(normalized) ||
                        tenant.Users.Any(u =>
                            u.DeletedAt == null &&
                            u.Email.ToLower().Contains(normalized)),

                TenantSearchTermKind.Numeric =>
                    tenant =>
                        tenant.PhoneNumber.Contains(token) ||
                        (tenant.IdentificationNumber != null &&
                         tenant.IdentificationNumber.ToLower().Contains(normalized)) ||
                        tenant.Users.Any(u =>
                            u.DeletedAt == null &&
                            (u.PhoneNumber.Contains(token) ||
                             u.IdentificationNumber.ToLower().Contains(normalized))),

                TenantSearchTermKind.Identification =>
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
