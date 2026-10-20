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

            AddIncludes();

            if (idPatient.GetValueOrDefault() > 0)
                Criteria = And(p => p.IdPatient == idPatient);

            if (idAppointment.GetValueOrDefault() > 0)
                Criteria = And(p => p.IdAppointment == idAppointment);

            if (idPatientTreatment.GetValueOrDefault() > 0)
                Criteria = And(p => p.IdPatientTreatment == idPatientTreatment);

            if (from.HasValue)
                Criteria = And(p => p.PaidAt >= from.Value);

            if (to.HasValue)
                Criteria = And(p => p.PaidAt <= to.Value);
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
                spec.Criteria = spec.And(p => p.IdAppointment == idAppointment);

            if (idPatientTreatment.GetValueOrDefault() > 0)
                spec.Criteria = spec.And(p => p.IdPatientTreatment == idPatientTreatment);

            return spec;
        }

        public static PaymentsSpec ForDashboardOutstanding(long idTenant)
        {
            return new PaymentsSpec
            {
                Criteria = p => p.IdTenant == idTenant,
                OrderByDescending = p => p.PaidAt
            };
        }

        public static PaymentsSpec ForDashboardSummary(
            long idTenant,
            DateTime from,
            DateTime to)
        {
            var spec = new PaymentsSpec
            {
                Criteria = p =>
                    p.IdTenant == idTenant &&
                    p.PaidAt >= from &&
                    p.PaidAt <= to,
                OrderByDescending = p => p.PaidAt
            };

            spec.AddInclude(p => p.PaymentMethod);

            return spec;
        }

        public static PaymentsSpec ExportPaymentsReport(
            long idTenant,
            long? idPatient = null,
            long? idAppointment = null,
            long? idPatientTreatment = null,
            DateOnly? from = null,
            DateOnly? to = null)
        {
            DateTime? fromDate = from?.ToDateTime(TimeOnly.MinValue);
            DateTime? toDate = to?.AddDays(1).ToDateTime(TimeOnly.MinValue);

            var spec = new PaymentsSpec
            {
                Criteria = p =>
                    p.IdTenant == idTenant &&
                    (!idPatient.HasValue || idPatient <= 0 || p.IdPatient == idPatient) &&
                    (!idAppointment.HasValue || idAppointment <= 0 || p.IdAppointment == idAppointment) &&
                    (!idPatientTreatment.HasValue || idPatientTreatment <= 0 || p.IdPatientTreatment == idPatientTreatment) &&
                    (!fromDate.HasValue || p.PaidAt >= fromDate.Value) &&
                    (!toDate.HasValue || p.PaidAt < toDate.Value),
                OrderByDescending = p => p.PaidAt
            };

            spec.AddIncludes();

            return spec;
        }

        private void AddIncludes()
        {
            AddInclude(p => p.Patient);
            AddInclude(p => p.Patient.IdentificationType);
            AddInclude(p => p.PaymentMethod);
            AddInclude(p => p.Appointment!);
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.AppointmentProcedures)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.AppointmentProcedures)}.{nameof(AppointmentProcedure.Procedure)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.AppointmentStatus)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.Professional)}");
            AddInclude($"{nameof(Payment.Appointment)}.{nameof(Appointment.Professional)}.{nameof(Professional.User)}");
            AddInclude(p => p.PatientTreatment!);
            AddInclude($"{nameof(Payment.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            AddInclude($"{nameof(Payment.PatientTreatment)}.{nameof(PatientTreatment.PatientTreatmentStatus)}");
        }

        private PaymentsSpec()
        {
        }
    }
}