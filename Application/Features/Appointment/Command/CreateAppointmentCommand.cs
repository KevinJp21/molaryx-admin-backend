using Application.Common.Mediator.Interfaces;

namespace Application.Features.Appointment.Command
{
    public class CreateAppointmentCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
        public long IdUser { get; set; }
        public long IdService { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string? Notes { get; set; }
    }
}
