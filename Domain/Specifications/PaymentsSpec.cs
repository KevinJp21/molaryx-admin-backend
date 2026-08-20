using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class PaymentsSpec : BaseSpecification<Payment>
    {
        public PaymentsSpec(
            long idTenant,
            long? idPatient = null,
            long? idAppointment = null,
            long? idPatientTreatment = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            Criteria = p => p.IdTenant == idTenant;
            OrderByDescending = p => p.PaidAt;
            AddInclude(p => p.Patient);
            AddInclude(p => p.Patient.IdentificationType);
            AddInclude(p => p.PaymentMethod);
            AddInclude(p => p.Appointment!);
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.Service)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.AppointmentStatus)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.Professional)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.Professional)}.{nameof(Professional.User)}");
            AddInclude(p => p.PatientTreatment!);
            AddInclude($"{nameof(Payment.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            AddInclude($"{nameof(Payment.PatientTreatment)}.{nameof(PatientTreatment.PatientTreatmentStatus)}");

            if (idPatient.GetValueOrDefault() > 0)
            {
                Criteria = And(p => p.IdPatient == idPatient);
            }

            if (idAppointment.GetValueOrDefault() > 0)
            {
                Criteria = And(p => p.IdAppointment == idAppointment);
            }

            if (idPatientTreatment.GetValueOrDefault() > 0)
            {
                Criteria = And(p => p.IdPatientTreatment == idPatientTreatment);
            }

            if (from.HasValue)
            {
                Criteria = And(p => p.PaidAt >= from.Value);
            }

            if (to.HasValue)
            {
                Criteria = And(p => p.PaidAt <= to.Value);
            }
        }

        public static PaymentsSpec ForConcept(
            long idTenant,
            long? idAppointment = null,
            long? idPatientTreatment = null)
        {
            var spec = new PaymentsSpec
            {
                Criteria = p => p.IdTenant == idTenant
            };

            if (idAppointment.GetValueOrDefault() > 0)
            {
                spec.Criteria = spec.And(p => p.IdAppointment == idAppointment);
            }

            if (idPatientTreatment.GetValueOrDefault() > 0)
            {
                spec.Criteria = spec.And(p => p.IdPatientTreatment == idPatientTreatment);
            }

            return spec;
        }

        private PaymentsSpec()
        {
        }
    }
}
