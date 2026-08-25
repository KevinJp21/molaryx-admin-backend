using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        [Column("id_notification")]
        public long IdNotification { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_user")]
        public long IdUser { get; set; }

        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Column("subject")]
        public string Subject { get; set; } = string.Empty;

        [Column("body")]
        public string Body { get; set; } = string.Empty;

        [Column("id_notification_status")]
        public short IdNotificationStatus { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
