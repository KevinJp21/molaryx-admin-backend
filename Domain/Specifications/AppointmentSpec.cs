using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Specifications
{
    public class AppointmentSpec : BaseSpecification<Appointment>
    {
        public static AppointmentSpec BySchedule(
            long idTenant,
            long idProfessional,
            DateTime startAt,
            DateTime endAt,
            long? excludeIdAppointment = null)
        {
            var pending = (short)AppointmentStatusEnum.PENDING;
            var confirmed = (short)AppointmentStatusEnum.CONFIRMED;
            var inProgress = (short)AppointmentStatusEnum.IN_PROGRESS;

            var spec = new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant &&
                    a.IdProfessional == idProfessional &&
                    a.StartAt < endAt &&
                    a.EndAt > startAt &&
                    (a.IdAppointmentStatus == pending
                        || a.IdAppointmentStatus == confirmed
                        || a.IdAppointmentStatus == inProgress) &&
                    (!excludeIdAppointment.HasValue || a.IdAppointment != excludeIdAppointment.Value)
            };
            return spec;
        }

        public static AppointmentSpec ForRange(long idTenant, DateTime from, DateTime to, long? idProfessional = null)
        {
            var spec = new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant &&
                    a.StartAt < to &&
                    a.EndAt > from &&
                    (!idProfessional.HasValue || a.IdProfessional == idProfessional.Value),
                OrderBy = a => a.StartAt
            };
            spec.AddInclude(a => a.Patient);
            spec.AddInclude(a => a.Service);
            spec.AddInclude(a => a.AppointmentStatus);
            spec.AddInclude(a => a.Professional);
            spec.AddInclude($"{nameof(Appointment.Professional)}.{nameof(Professional.User)}");
            spec.AddInclude($"{nameof(Appointment.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            return spec;
        }

        public static AppointmentSpec ForList(
            long idTenant,
            long? idPatient = null,
            long? idProfessional = null,
            short? idAppointmentStatus = null)
        {
            var spec = new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant
                    && (idPatient.GetValueOrDefault() <= 0 || a.IdPatient == idPatient)
                    && (idProfessional.GetValueOrDefault() <= 0 || a.IdProfessional == idProfessional)
                    && (!idAppointmentStatus.HasValue || a.IdAppointmentStatus == idAppointmentStatus),
                OrderByDescending = a => a.StartAt
            };
            spec.AddInclude(a => a.Patient);
            spec.AddInclude(a => a.Service);
            spec.AddInclude(a => a.AppointmentStatus);
            spec.AddInclude(a => a.Professional);
            spec.AddInclude($"{nameof(Appointment.Professional)}.{nameof(Professional.User)}");
            spec.AddInclude($"{nameof(Appointment.PatientTreatment)}.{nameof(PatientTreatment.Treatment)}");
            return spec;
        }

        public static AppointmentSpec ById(long idAppointment)
        {
            return new AppointmentSpec
            {
                Criteria = a => a.IdAppointment == idAppointment
            };
        }

        public static AppointmentSpec ForDashboardPeriod(
            long idTenant,
            DateTime from,
            DateTime to)
        {
            var spec = new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant
                    && a.StartAt >= from
                    && a.StartAt <= to,
                OrderBy = a => a.StartAt
            };
            spec.AddInclude(a => a.Service);
            spec.AddInclude(a => a.AppointmentStatus);
            return spec;
        }

        public static AppointmentSpec ForDashboardOutstanding(long idTenant)
        {
            var cancelled = (short)AppointmentStatusEnum.CANCELLED;

            return new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant
                    && a.Price != null
                    && a.IdAppointmentStatus != cancelled
            };
        }

        public static AppointmentSpec ForDashboardUpcoming(long idTenant, DateTime from)
        {
            var spec = new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant
                    && a.StartAt >= from,
                OrderBy = a => a.StartAt
            };
            spec.AddInclude(a => a.Patient);
            spec.AddInclude(a => a.Service);
            spec.AddInclude(a => a.AppointmentStatus);
            spec.AddInclude(a => a.Professional);
            spec.AddInclude($"{nameof(Appointment.Professional)}.{nameof(Professional.User)}");
            return spec;
        }

        private AppointmentSpec()
        {
        }
    }
}
