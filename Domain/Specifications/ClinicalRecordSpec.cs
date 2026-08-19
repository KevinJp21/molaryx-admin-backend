using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ClinicalRecordSpec : BaseSpecification<ClinicalRecord>
    {
        public static ClinicalRecordSpec ForList(
            long idTenant,
            long idPatient,
            long? idAppointment = null,
            long? idPatientTreatment = null)
        {
            var spec = new ClinicalRecordSpec
            {
                Criteria = c =>
                    c.IdTenant == idTenant
                    && c.IdPatient == idPatient
                    && (idAppointment.GetValueOrDefault() <= 0 || c.IdAppointment == idAppointment)
                    && (idPatientTreatment.GetValueOrDefault() <= 0 || c.IdPatientTreatment == idPatientTreatment),
                OrderByDescending = c => c.RecordedAt
            };
            spec.AddInclude(c => c.Patient);
            spec.AddInclude(c => c.Service!);
            spec.AddInclude(c => c.CreatedByUser);
            spec.AddInclude($"{nameof(ClinicalRecord.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            return spec;
        }

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