using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class UserSpec : BaseSpecification<User>
    {
        public UserSpec(long idUser)
        {
            Criteria = u => u.IdUser == idUser;
            AddProfileIncludes();
        }

        public static UserSpec ForTeam(
            long idTenant,
            IReadOnlyCollection<short>? idUserRoles = null,
            short? idUserStatus = null,
            string? search = null)
        {
            var spec = new UserSpec
            {
                Criteria = u => u.IdTenant == idTenant,
                OrderBy = u => u.FirstName
            };
            spec.AddInclude(u => u.UserStatus);
            spec.AddInclude(u => u.IdentificationType);
            spec.AddInclude(u => u.Professional!);

            if (idUserRoles is { Count: > 0 })
            {
                var roles = idUserRoles.ToArray();
                spec.Criteria = spec.And(u => roles.Contains(u.IdUserRole));
            }

            if (idUserStatus != null)
            {
                spec.Criteria = spec.And(u => u.IdUserStatus == idUserStatus);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                foreach (var token in SearchText.Tokens(search))
                {
                    spec.Criteria = spec.And(u =>
                        u.Username.ToLower().Contains(token) ||
                        u.FirstName.ToLower().Contains(token) ||
                        (u.SecondName != null && u.SecondName.ToLower().Contains(token)) ||
                        u.FirstSurname.ToLower().Contains(token) ||
                        (u.SecondSurname != null && u.SecondSurname.ToLower().Contains(token)));
                }
            }

            return spec;
        }

        public static UserSpec ByEmail(string email)
        {
            var spec = new UserSpec
            {
                Criteria = u => u.Email == email
            };
            spec.AddProfileIncludes();
            return spec;
        }

        public static UserSpec ByUsername(string username)
        {
            var spec = new UserSpec
            {
                Criteria = u => u.Username == username
            };
            return spec;
        }

        public static UserSpec ByIdentificationNumber(string identificationNumber)
        {
            var spec = new UserSpec
            {
                Criteria = u => u.IdentificationNumber == identificationNumber
            };
            return spec;
        }

        public static UserSpec ByPhoneNumber(string phoneNumber)
        {
            var spec = new UserSpec
            {
                Criteria = u => u.PhoneNumber == phoneNumber
            };
            return spec;
        }

        private UserSpec()
        {
        }

        private void AddProfileIncludes()
        {
            AddInclude(u => u.UserRole);
            AddInclude(u => u.UserStatus);
            AddInclude(u => u.Tenant);
        }
    }
}
