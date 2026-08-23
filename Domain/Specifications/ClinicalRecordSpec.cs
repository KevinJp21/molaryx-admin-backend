using Domain.Common;
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
            DateTime? recordedFrom = null,
            DateTime? recordedToInclusive = null,
            bool orderAscending = false)
        {
            Criteria = c => c.IdTenant == idTenant;

            if (orderAscending)
            {
                OrderBy = c => c.RecordedAt;
            }
            else
            {
                OrderByDescending = c => c.RecordedAt;
            }

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
            AddInclude(c => c.PatientTreatment!);
            AddInclude($"{nameof(ClinicalRecord.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            AddInclude($"{nameof(ClinicalRecord.PatientTreatment)}.{nameof(PatientTreatment.PatientTreatmentStatus)}");

            if (idPatient.GetValueOrDefault() > 0)
            {
                Criteria = And(c => c.IdPatient == idPatient);
            }

            if (idAppointment.GetValueOrDefault() > 0)
            {
                Criteria = And(c => c.IdAppointment == idAppointment);
            }

            if (idPatientTreatment.GetValueOrDefault() > 0)
            {
                Criteria = And(c => c.IdPatientTreatment == idPatientTreatment);
            }

            if (recordedFrom.HasValue)
            {
                Criteria = And(c => c.RecordedAt >= recordedFrom.Value);
            }

            if (recordedToInclusive.HasValue)
            {
                Criteria = And(c => c.RecordedAt <= recordedToInclusive.Value);
            }
        }

        public static ClinicalRecordSpec ById(long idClinicalRecord)
        {
            return new ClinicalRecordSpec
            {
                Criteria = c => c.IdClinicalRecord == idClinicalRecord
            };
        }

        private ClinicalRecordSpec()
        {
        }
    }
}
