using Domain.Common;
using Domain.Common.Patients;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Specifications
{
    public class PatientTreatmentsSpec : BaseSpecification<PatientTreatment>
    {
        public PatientTreatmentsSpec(
            long idTenant,
            long? idPatient = null,
            string? search = null,
            short? idPatientTreatmentStatus = null)
        {

            Criteria = pt =>
                pt.IdTenant == idTenant
                && pt.Patient.DeletedAt == null
                && (!idPatient.HasValue || pt.IdPatient == idPatient.Value)
                && (!idPatientTreatmentStatus.HasValue || pt.IdPatientTreatmentStatus == idPatientTreatmentStatus.Value);
            OrderByDescending = pt => pt.StartAt;

            AddInclude(pt => pt.Patient);
            AddInclude(pt => pt.Treatment);
            AddInclude(pt => pt.PatientTreatmentStatus);
            AddInclude(pt => pt.PaymentFrequency!);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var tokens = PatientSearch.GetTokens(search);

                foreach (var token in tokens)
                {
                    Criteria = And(PatientSearch.MatchesPatientTreatmentToken(token));
                }
            }
        }

        public static PatientTreatmentsSpec ById(long idPatientTreatment)
        {
            return ForDetail(idPatientTreatment);
        }

        public static PatientTreatmentsSpec ForDetail(long idPatientTreatment)
        {
            var spec = new PatientTreatmentsSpec
            {
                Criteria = pt =>
                    pt.IdPatientTreatment == idPatientTreatment
                    && pt.Patient.DeletedAt == null
            };
            spec.AddInclude(pt => pt.Treatment);
            spec.AddInclude(pt => pt.PatientTreatmentStatus);
            spec.AddInclude(pt => pt.PaymentFrequency!);
            return spec;
        }

        public static PatientTreatmentsSpec ActiveByPatientAndTreatment(
            long idTenant,
            long idPatient,
            long idTreatment,
            long? excludeIdPatientTreatment = null)
        {
            return new PatientTreatmentsSpec
            {
                Criteria = pt =>
                    pt.IdTenant == idTenant
                    && pt.IdPatient == idPatient
                    && pt.Patient.DeletedAt == null
                    && pt.IdTreatment == idTreatment
                    && (pt.IdPatientTreatmentStatus == (short)PatientTreatmentStatusEnum.ACTIVE
                        || pt.IdPatientTreatmentStatus == (short)PatientTreatmentStatusEnum.PAUSED)
                    && (!excludeIdPatientTreatment.HasValue
                        || pt.IdPatientTreatment != excludeIdPatientTreatment.Value)
            };
        }

        private PatientTreatmentsSpec()
        {
        }
    }
}
