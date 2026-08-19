namespace Application.Features.Payment.Query.GetPayments
{
    public class GetPaymentsResponse
    {
        public long IdPayment { get; set; }
        public long IdPatient { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public short IdPaymentMethod { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public PaymentAppointment? Appointment { get; set; }
        public PaymentPatientTreatment? PatientTreatment { get; set; }
    }

    public class PaymentAppointment
    {
        public long IdAppointment { get; set; }
        public long IdService { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public short IdAppointmentStatus { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string ProfessionalName { get; set; } = string.Empty;
        public string ProfessionalSurname { get; set; } = string.Empty;
    }

    public class PaymentPatientTreatment
    {
        public long IdPatientTreatment { get; set; }
        public long IdTreatment { get; set; }
        public string TreatmentName { get; set; } = string.Empty;
        public decimal? AgreedPrice { get; set; }
        public short IdPatientTreatmentStatus { get; set; }
        public string PatientTreatmentStatus { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
