using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class TenantSpec : BaseSpecification<Tenant>
    {
        public static TenantSpec ByEmail(string email)
        {
            var spec = new TenantSpec
            {
                Criteria = t => t.Email == email
            };
            return spec;
        }

        public static TenantSpec ByIdentificationNumber(string identificationNumber)
        {
            var spec = new TenantSpec
            {
                Criteria = t => t.IdentificationNumber == identificationNumber
            };
            return spec;
        }

        public static TenantSpec ByPhoneNumber(string phoneNumber)
        {
            var spec = new TenantSpec
            {
                Criteria = t => t.PhoneNumber == phoneNumber
            };
            return spec;
        }

        private TenantSpec()
        {
        }
    }
}
