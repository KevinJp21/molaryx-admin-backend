namespace Application.Features.Payment.Query.GetPaymentReport
{
    public sealed class PaymentReportFileResult
    {
        public required byte[] Content { get; init; }
        public required string FileName { get; init; }
    }
}
