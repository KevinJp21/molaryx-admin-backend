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
            return spec;
        }

        public static AppointmentSpec ById(long idAppointment)
        {
            return new AppointmentSpec
            {
                Criteria = a => a.IdAppointment == idAppointment
            };
        }

        private AppointmentSpec()
        {
        }
    }
}
