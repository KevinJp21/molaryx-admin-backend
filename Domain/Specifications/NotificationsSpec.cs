using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class NotificationsSpec : BaseSpecification<Notification>
    {
        public NotificationsSpec(long? idTenant, long idUser, bool onlyUnviewed = false)
        {
            Criteria = idTenant.HasValue
                ? n => n.IdTenant == idTenant && n.IdUser == idUser
                : n => n.IdTenant == null && n.IdUser == idUser;

            if (onlyUnviewed)
            {
                Criteria = And(n => !n.IsViewed);
            }

            OrderByDescending = n => n.CreatedAt;
        }

        public static NotificationsSpec ById(long? idTenant, long idUser, long idNotification) => new()
        {
            Criteria = idTenant.HasValue
                ? n =>
                    n.IdNotification == idNotification
                    && n.IdTenant == idTenant
                    && n.IdUser == idUser
                : n =>
                    n.IdNotification == idNotification
                    && n.IdTenant == null
                    && n.IdUser == idUser
        };

        public static NotificationsSpec UnreadByUser(long? idTenant, long idUser) => new()
        {
            Criteria = idTenant.HasValue
                ? n =>
                    n.IdTenant == idTenant
                    && n.IdUser == idUser
                    && !n.IsViewed
                : n =>
                    n.IdTenant == null
                    && n.IdUser == idUser
                    && !n.IsViewed
        };

        private NotificationsSpec()
        {
        }
    }
}
