using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Specifications
{
    public class PatientTreatmentsSpec : BaseSpecification<PatientTreatment>
    {
        public static PatientTreatmentsSpec ActiveByPatientAndTreatment(
            long idTenant,
            long idPatient,
            long idTreatment)
        {
            return new PatientTreatmentsSpec
            {
                Criteria = pt =>
                    pt.IdTenant == idTenant
                    && pt.IdPatient == idPatient
                    && pt.IdTreatment == idTreatment
                    && (pt.IdTreatmentStatus == (short)TreatmentStatusEnum.ACTIVE
                        || pt.IdTreatmentStatus == (short)TreatmentStatusEnum.PAUSED)
            };
        }

        private PatientTreatmentsSpec()
        {
        }
    }
}
