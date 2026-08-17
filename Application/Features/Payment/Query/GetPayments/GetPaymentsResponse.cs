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
    }
}
