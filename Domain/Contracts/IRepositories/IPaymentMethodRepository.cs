using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod, short>
    {
    }
}
