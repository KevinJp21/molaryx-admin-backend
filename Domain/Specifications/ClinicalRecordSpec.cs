
using Domain.Common;
using Domain.Common.ClinicalRecords;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ClinicalRecordSpec : BaseSpecification<ClinicalRecord>
    {
        public ClinicalRecordSpec(
            long idTenant,
            long? idPatient = null,
            long? idAppointment = null,
            long? idPatientTreatment = null,
            string? search = null,
            DateOnly? from = null,
            DateOnly? to = null)
        {
            Criteria = c => c.IdTenant == idTenant;

            AddIncludes();

            if (idPatient is > 0)
                Criteria = And(c => c.IdPatient == idPatient.Value);

            if (idAppointment is > 0)
                Criteria = And(c => c.IdAppointment == idAppointment.Value);

            if (idPatientTreatment is > 0)
                Criteria = And(c => c.IdPatientTreatment == idPatientTreatment.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var tokens = Search.GetTokens(search);

                foreach (var token in tokens)
                    Criteria = And(ClinicalRecordSearch.MatchesToken(token));
            }

            if (from is not null)
            {
                DateTime fromDate = from.Value.ToDateTime(TimeOnly.MinValue);

                Criteria = And (c => c.RecordedAt >= fromDate);
            }

            if (to is not null)
            {
                DateTime toDate = to.Value.AddDays(1).ToDateTime(TimeOnly.MinValue);

                Criteria = And (c => c.RecordedAt < toDate);
            }


        }

        public static ClinicalRecordSpec ById(long idClinicalRecord)
        {
            return new ClinicalRecordSpec
            {
                Criteria = c => c.IdClinicalRecord == idClinicalRecord
            };
        }

        public static ClinicalRecordSpec ExportClinicalHistory(
            long idTenant,
            long idPatient,
            DateOnly? from = null,
            DateOnly? to = null)
        {
            DateTime? fromDate = from?.ToDateTime(TimeOnly.MinValue);
            DateTime? toDate = to?.AddDays(1).ToDateTime(TimeOnly.MinValue);

            var spec = new ClinicalRecordSpec
            {
                Criteria = c =>
                    c.IdTenant == idTenant &&
                    c.IdPatient == idPatient &&
                    (!fromDate.HasValue || c.RecordedAt >= fromDate.Value) &&
                    (!toDate.HasValue || c.RecordedAt < toDate.Value),

                    OrderByDescending = c => c.RecordedAt
            };

            spec.AddIncludes();

            return spec;
        }

        private void AddIncludes()
        {
            AddInclude(c => c.Patient);
            AddInclude(c => c.Patient.IdentificationType);
            AddInclude(c => c.Procedure!);
            AddInclude(c => c.CreatedByUser);
            AddInclude(c => c.Appointment!);
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.AppointmentProcedures)}");
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.AppointmentProcedures)}.{nameof(AppointmentProcedure.Procedure)}");
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.AppointmentStatus)}");
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.Professional)}");
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.Professional)}.{nameof(Professional.User)}");
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.PatientTreatment)}");
            AddInclude($"{nameof(ClinicalRecord.Appointment)}.{nameof(Appointment.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            AddInclude(c => c.PatientTreatment!);
            AddInclude($"{nameof(ClinicalRecord.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            AddInclude($"{nameof(ClinicalRecord.PatientTreatment)}.{nameof(PatientTreatment.PatientTreatmentStatus)}");
        }

        private ClinicalRecordSpec()
        {
        }
    }
}
