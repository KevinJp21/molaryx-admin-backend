namespace Application.Features.Dashboard.Query.GetPaymentsSummary
{
    public class GetPaymentsSummaryResponse
    {
        public decimal CurrentMonthRevenue { get; set; }
        public decimal OutstandingBalance { get; set; }
        public List<RevenueOverTimeItem> RevenueOverTime { get; set; } = [];
        public List<PaymentMethodShare> PaymentMethods { get; set; } = [];
    }

    public class RevenueOverTimeItem
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
    }

    public class PaymentMethodShare
    {
        public short IdPaymentMethod { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int PaymentCount { get; set; }
    }
}
