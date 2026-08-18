namespace Application.Features.Payment.Query.GetPaymentsSummaryByConcept
{
    public class GetPaymentsSummaryByConceptResponse
    {
        public decimal? Price { get; set; }
        public decimal? AgreedPrice { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal? Remaining { get; set; }
        public decimal? Credit { get; set; }
        public int PaymentCount { get; set; }
    }
}
