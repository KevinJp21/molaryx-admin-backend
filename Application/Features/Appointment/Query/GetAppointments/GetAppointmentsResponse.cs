namespace Application.Features.Appointment.Query.GetAppointments
{
    public class GetAppointmentsResponse
    {
        public long IdAppointment { get; set; }
        public long IdPatient { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientSurname { get; set; } = string.Empty;
        public long IdProfessional { get; set; }
        public long IdUser { get; set; }
        public string ProfessionalName { get; set; } = string.Empty;
        public string ProfessionalSurname { get; set; } = string.Empty;
        public long IdService { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public short IdAppointmentStatus { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string? Notes { get; set; }
    }
}
