using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Payment.Command.CreatePayment
{
    public class CreatePaymentCommandHandler(
        IPaymentService _paymentService
    ) : IRequestHandler<CreatePaymentCommand, bool>
    {
        public Task<bool> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return _paymentService.CreatePaymentAsync(request, cancellationToken);
        }
    }
}
