using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ClinicalRecordSpec : BaseSpecification<ClinicalRecord>
    {
        public static ClinicalRecordSpec ById(long idClinicalRecord)
        {
            var spec = new ClinicalRecordSpec
            {
                Criteria = c => c.IdClinicalRecord == idClinicalRecord
            };
            return spec;
        }

        private ClinicalRecordSpec()
        {
        }
    }
}