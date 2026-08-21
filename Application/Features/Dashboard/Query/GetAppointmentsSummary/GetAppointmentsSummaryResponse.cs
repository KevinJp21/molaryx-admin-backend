namespace Application.Features.Dashboard.Query.GetAppointmentsSummary
{
    public class GetAppointmentsSummaryResponse
    {
        public int TodayCount { get; set; }
        public List<AppointmentStatusShare> ByStatus { get; set; } = [];
        public List<TopServiceItem> TopServices { get; set; } = [];
        public List<UpcomingAppointmentItem> Upcoming { get; set; } = [];
    }

    public class AppointmentStatusShare
    {
        public short IdAppointmentStatus { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class TopServiceItem
    {
        public long IdService { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class UpcomingAppointmentItem
    {
        public long IdAppointment { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientSurname { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ProfessionalName { get; set; } = string.Empty;
        public string ProfessionalSurname { get; set; } = string.Empty;
        public short IdAppointmentStatus { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
    }
}
