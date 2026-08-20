using Domain.Models;

namespace Domain.Contracts.IServices
{
    public interface IPaymentReportService
    {
        byte[] GeneratePaymentReport(PaymentReportInformation information);
    }
}
