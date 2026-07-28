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

        [Column("starts_at")]
        public DateTime? StartsAt { get; set; }

        [Column("ends_at")]
        public DateTime? EndsAt  { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public Plan Plan { get; set; } = null!;
        public TenantSubscriptionStatus TenantSubscriptionStatus { get; set; } = null!;
    }
}