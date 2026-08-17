using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PaymentRepository(AppDbContext context)
        : BaseRepository<Payment, long>(context), IPaymentRepository
    {
    }
}
