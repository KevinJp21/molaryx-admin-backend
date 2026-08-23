using Application.Common.Mediator.Interfaces;
using Application.Features.Appointment.Command.CreateAppointment;

namespace Application.Features.Appointment.Command.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<bool>
    {
        public long IdAppointment { get; set; }
        public long? IdPatient { get; set; }
        public long? IdUser { get; set; }
        public long? IdPatientTreatment { get; set; }
        public List<AppointmentProcedureItem>? Procedures { get; set; }
        public short? IdAppointmentStatus { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string? Notes { get; set; }
    }
}
