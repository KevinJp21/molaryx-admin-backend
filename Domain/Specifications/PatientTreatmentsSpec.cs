using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Specifications
{
    public class PatientTreatmentsSpec : BaseSpecification<PatientTreatment>
    {
        public static PatientTreatmentsSpec ForPatient(
            long idTenant,
            long? idPatient = null,
            short? idTreatmentStatus = null)
        {
            var spec = new PatientTreatmentsSpec
            {
                Criteria = pt =>
                    pt.IdTenant == idTenant
                    && (!idPatient.HasValue || pt.IdPatient == idPatient.Value)
                    && (!idTreatmentStatus.HasValue || pt.IdTreatmentStatus == idTreatmentStatus.Value),
                OrderByDescending = pt => pt.StartAt
            };
            spec.AddInclude(pt => pt.Patient);
            spec.AddInclude(pt => pt.Treatment);
            spec.AddInclude(pt => pt.TreatmentStatus);
            spec.AddInclude(pt => pt.PaymentFrequency!);
            return spec;
        }

        public static PatientTreatmentsSpec ById(long idPatientTreatment)
        {
            return ForDetail(idPatientTreatment);
        }

        public static PatientTreatmentsSpec ForDetail(long idPatientTreatment)
        {
            var spec = new PatientTreatmentsSpec
            {
                Criteria = pt => pt.IdPatientTreatment == idPatientTreatment
            };
            spec.AddInclude(pt => pt.Treatment);
            spec.AddInclude(pt => pt.TreatmentStatus);
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
                    && pt.IdTreatment == idTreatment
                    && (pt.IdTreatmentStatus == (short)TreatmentStatusEnum.ACTIVE
                        || pt.IdTreatmentStatus == (short)TreatmentStatusEnum.PAUSED)
                    && (!excludeIdPatientTreatment.HasValue
                        || pt.IdPatientTreatment != excludeIdPatientTreatment.Value)
            };
        }

        private PatientTreatmentsSpec()
        {
        }
    }
}
