namespace Application.Features.PatientTreatment.Query.GetPatientTreatments
{
    public class GetPatientTreatmentsResponse
    {
        public long IdPatientTreatment { get; set; }
        public long IdPatient { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientSurname { get; set; } = string.Empty;
        public long IdTreatment { get; set; }
        public string TreatmentName { get; set; } = string.Empty;
        public decimal? AgreedPrice { get; set; }
        public short? IdPaymentFrequency { get; set; }
        public string? PaymentFrequency { get; set; }
        public decimal? PeriodicAmount { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public short IdTreatmentStatus { get; set; }
        public string TreatmentStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
