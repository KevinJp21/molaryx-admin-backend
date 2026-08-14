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
