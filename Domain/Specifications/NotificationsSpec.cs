using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Specifications
{
    public class NotificationsSpec : BaseSpecification<Notification>
    {
        public NotificationsSpec(long idTenant, long idUser, bool onlyUnviewed = false)
        {
            Criteria = n =>
                n.IdTenant == idTenant
                && n.IdUser == idUser;

            if (onlyUnviewed)
            {
                Criteria = And(n => n.IdNotificationStatus == (short)NotificationStatusEnum.UNREAD);
            }

            OrderByDescending = n => n.CreatedAt;
        }

        public static NotificationsSpec ById(long idTenant, long idUser, long idNotification) => new()
        {
            Criteria = n =>
                n.IdNotification == idNotification
                && n.IdTenant == idTenant
                && n.IdUser == idUser
        };

        public static NotificationsSpec UnreadByUser(long idTenant, long idUser) => new()
        {
            Criteria = n =>
                n.IdTenant == idTenant
                && n.IdUser == idUser
                && n.IdNotificationStatus == (short)NotificationStatusEnum.UNREAD
        };

        private NotificationsSpec()
        {
        }
    }
}
