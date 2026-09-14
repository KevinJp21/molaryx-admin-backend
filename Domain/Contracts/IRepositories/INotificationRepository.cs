using Domain.Common;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
    public interface INotificationRepository : IBaseRepository<Notification, long>
    {
    }
}
