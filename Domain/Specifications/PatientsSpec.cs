using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PatientsSpec : BaseSpecification<Patient>
    {
        public PatientsSpec(long idTenant, bool? isActive, string? search = null)
        {
            Criteria = p => p.IdTenant == idTenant && p.DeletedAt == null;
            AddInclude(p => p.IdentificationType);
            OrderByDescending = p => p.CreatedAt;

            if (isActive == true)
            {
                Criteria = And(p => p.IsActive == true);
            }
            else if (isActive == false)
            {
                Criteria = And(p => p.IsActive == false);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                foreach (var token in SearchText.Tokens(search))
                {
                    Criteria = And(p =>
                        p.FirstName.ToLower().Contains(token) ||
                        (p.SecondName != null && p.SecondName.ToLower().Contains(token)) ||
                        p.FirstSurname.ToLower().Contains(token) ||
                        (p.SecondSurname != null && p.SecondSurname.ToLower().Contains(token)) ||
                        p.IdentificationNumber.ToLower().Contains(token) ||
                        p.Email.ToLower().Contains(token) ||
                        p.PhoneNumber.ToLower().Contains(token));
                }
            }
        }

        public static PatientsSpec ById(long idPatient)
        {
            var spec = new PatientsSpec
            {
                Criteria = p => p.IdPatient == idPatient && p.DeletedAt == null
            };
            return spec;
        }

        public static PatientsSpec ByEmail(long idTenant, string email)
        {
            var spec = new PatientsSpec
            {
                Criteria = p =>
                    p.IdTenant == idTenant
                    && p.DeletedAt == null
                    && p.Email == email
            };
            return spec;
        }

        public static PatientsSpec ByPhoneNumber(long idTenant, string phoneNumber)
        {
            var spec = new PatientsSpec
            {
                Criteria = p =>
                    p.IdTenant == idTenant
                    && p.DeletedAt == null
                    && p.PhoneNumber == phoneNumber
            };
            return spec;
        }

        public static PatientsSpec ByIdentificationNumber(long idTenant, string identificationNumber)
        {
            var spec = new PatientsSpec
            {
                Criteria = p =>
                    p.IdTenant == idTenant
                    && p.DeletedAt == null
                    && p.IdentificationNumber == identificationNumber
            };
            return spec;
        }

        public static PatientsSpec ForTenantCount(long idTenant)
        {
            var spec = new PatientsSpec
            {
                Criteria = p => p.IdTenant == idTenant && p.DeletedAt == null
            };
            return spec;
        }

        private PatientsSpec()
        {
        }
    }
}
