using Application.Common.Mediator.Interfaces;

namespace Application.Features.Appointment.Command.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<bool>
    {
        public long IdAppointment { get; set; }
        public long? IdPatient { get; set; }
        public long? IdUser { get; set; }
        public long? IdService { get; set; }
        public long? IdPatientTreatment { get; set; }
        public decimal? Price { get; set; }
        public short? IdAppointmentStatus { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string? Notes { get; set; }
    }
}
