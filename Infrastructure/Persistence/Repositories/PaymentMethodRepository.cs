using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PaymentMethodRepository(AppDbContext context)
        : BaseRepository<PaymentMethod, short>(context), IPaymentMethodRepository
    {
    }
}
