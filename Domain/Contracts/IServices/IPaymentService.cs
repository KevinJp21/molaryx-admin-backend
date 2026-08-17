using Application.Features.Payment.Command.CreatePayment;

namespace Domain.Contracts.IServices
{
    public interface IPaymentService
    {
        Task<bool> CreatePaymentAsync(
            CreatePaymentCommand request,
            CancellationToken cancellationToken);
    }
}
