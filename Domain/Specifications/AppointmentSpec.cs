using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class AppointmentSpec : BaseSpecification<Appointment>
    {
        public static AppointmentSpec BySchedule(long idTenant, long idProfessional, DateTime startAt, DateTime endAt)
        {
            var spec = new AppointmentSpec
            {
                Criteria = a => a.IdTenant == idTenant && 
                a.IdProfessional == idProfessional && 
                a.StartAt < endAt && 
                a.EndAt > startAt
            };
            return spec;
        }

        private AppointmentSpec()
        {
        }
    }
}