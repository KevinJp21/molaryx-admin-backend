namespace Domain.Models
{
    public class PaymentReportInformation
    {
        public string ConsultoryName { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }
        public DateOnly? PeriodFrom { get; set; }
        public DateOnly? PeriodTo { get; set; }
        public List<PaymentReportRowInformation> Rows { get; set; } = [];
    }

    public class PaymentReportRowInformation
    {
        public DateTime PaidAt { get; set; }
        public string PatientFullName { get; set; } = string.Empty;
        public string IdentificationTypeCode { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Concept { get; set; } = string.Empty;
        public string TreatmentName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
