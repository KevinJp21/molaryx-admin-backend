using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Promotion : BaseEntity
    {
        [Column("id_promotion")]
        public long IdPromotion { get; set; }

        [Column("code")]
        public string Code { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("starts_at")]
        public DateTime StartsAt { get; set; }

        [Column("ends_at")]
        public DateTime? EndsAt { get; set; }

        [Column("duration_months")]
        public short DurationMonths { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<PromotionPlan> PromotionPlans { get; set; } = [];
        public ICollection<TenantSubscription> tenantSubscriptions { get; set; } = [];
    }
}