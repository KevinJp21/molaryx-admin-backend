namespace Application.Features.Payment.Query.GetPayments
{
    public class GetPaymentsResponse
    {
        public long IdPayment { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public short IdPaymentMethod { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public Patient Patient { get; set; } = null!;
        public PaymentAppointment? Appointment { get; set; }
        public PaymentPatientTreatment? PatientTreatment { get; set; }
    }

    public class Patient
    {
        public long IdPatient { get; set; }
        public string IdentificationType { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class PaymentAppointment
    {
        public long IdAppointment { get; set; }
        public string ProcedureNames { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
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
