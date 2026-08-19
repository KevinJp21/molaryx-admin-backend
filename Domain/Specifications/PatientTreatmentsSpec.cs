using Domain.Common;
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
            short? idTreatmentStatus = null)
        {

            Criteria = pt =>
                pt.IdTenant == idTenant
                && (!idPatient.HasValue || pt.IdPatient == idPatient.Value)
                && (!idTreatmentStatus.HasValue || pt.IdTreatmentStatus == idTreatmentStatus.Value);
            OrderByDescending = pt => pt.StartAt;

            AddInclude(pt => pt.Patient);
            AddInclude(pt => pt.Treatment);
            AddInclude(pt => pt.TreatmentStatus);
            AddInclude(pt => pt.PaymentFrequency!);

            if (!string.IsNullOrWhiteSpace(search))
            {
                foreach (var token in SearchText.Tokens(search))
                {
                    Criteria = And(pt =>
                        pt.Treatment.Name.ToLower().Contains(token) ||
                        pt.Patient.FirstName.ToLower().Contains(token) ||
                        (pt.Patient.SecondName != null && pt.Patient.SecondName.ToLower().Contains(token)) ||
                        pt.Patient.FirstSurname.ToLower().Contains(token) ||
                        (pt.Patient.SecondSurname != null && pt.Patient.SecondSurname.ToLower().Contains(token)) ||
                        pt.Patient.IdentificationNumber.ToLower().Contains(token) ||
                        pt.Patient.Email.ToLower().Contains(token) ||
                        pt.Patient.PhoneNumber.ToLower().Contains(token));
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
