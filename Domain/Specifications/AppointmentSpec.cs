using Domain.Common;
using Domain.Entities;

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
            var spec = new AppointmentSpec
            {
                Criteria = a =>
                    a.IdTenant == idTenant &&
                    a.IdProfessional == idProfessional &&
                    a.StartAt < endAt &&
                    a.EndAt > startAt &&
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
        private AppointmentSpec()
        {
        }
    }
}