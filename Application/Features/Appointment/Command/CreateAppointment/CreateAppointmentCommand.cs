using Application.Common.Mediator.Interfaces;

namespace Application.Features.Appointment.Command.CreateAppointment
{
    public class AppointmentProcedureItem
    {
        public long IdProcedure { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateAppointmentCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
        public long IdUser { get; set; }
        public long? IdPatientTreatment { get; set; }
        public List<AppointmentProcedureItem> Procedures { get; set; } = [];
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string? Notes { get; set; }
    }
}
