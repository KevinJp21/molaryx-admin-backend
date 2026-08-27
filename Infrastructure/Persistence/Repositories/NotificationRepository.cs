using Domain.Contracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class NotificationRepository(AppDbContext context)
        : BaseRepository<Notification, long>(context), INotificationRepository
    {
    }
}
