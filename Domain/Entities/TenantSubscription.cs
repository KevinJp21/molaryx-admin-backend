using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class TenantSubscription : BaseEntity
    {
        [Column("id_tenant_subscription")]
        public long IdTenantSubscription { get; set; }

        [Column("id_tenant_subscription_status")]
        public short IdTenantSubscriptionStatus { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_plan")]
        public short IdPlan { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("max_professionals")]
        public short? MaxProfessionals { get; set; }

        [Column("max_assistants")]
        public short? MaxAssistants { get; set; }

        [Column("max_patients")]
        public int? MaxPatients { get; set; }

        [Column("started_at")]
        public DateTime StartedAt { get; set; }

        [Column("expires_at")]
        public DateTime? ExpiresAt { get; set; }

        // Navigation properties
        public Tenant Tenants { get; set; } = null!;
        public Plan Plans { get; set; } = null!;
        public TenantSubscriptionStatus TenantSubscriptionStatuses { get; set; } = null!;
    }
}