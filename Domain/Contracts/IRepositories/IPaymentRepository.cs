using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface IPaymentRepository : IBaseRepository<Payment, long>
    {
    }
}
