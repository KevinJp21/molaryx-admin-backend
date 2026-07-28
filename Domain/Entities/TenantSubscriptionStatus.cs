using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class TenantSubscriptionStatus : BaseEntity
    {
        [Column("id_tenant_subscription_status")]
        public short IdTenantSubscriptionStatus { get; set; }

        [Column("code")]
        public string Code { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<TenantSubscription> TenantSubscriptions { get; set; } = [];
    }
}