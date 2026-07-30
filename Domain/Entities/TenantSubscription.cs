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

        [Column("id_promotion")]
        public long? IdPromotion { get; set; }

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
        public DateTime? EndsAt { get; set; }

        /*Fecha hasta la que el cliente conserva el beneficio de la promoción.
        Al superar esta fecha, deja de aplicar el precio promocional
        y se debe utilizar el precio normal del plan.*/
        [Column("promotion_ends_at")]
        public DateTime? PromotionEndsAt { get; set; }

        /* Indica si el beneficio promocional de esta suscripción
        se encuentra vigente actualmente.
        
         La promoción debe:
         - Estar asociada a la suscripción.
         - Tener una fecha de inicio.
         - Tener una fecha de finalización.
         - Haber iniciado.
         - No haber finalizado.
        
         Esta propiedad no se almacena en la base de datos porque
         su valor se calcula dinámicamente a partir de las fechas.*/
        [NotMapped]
        public bool IsPromotionActive =>
            IdPromotion.HasValue &&
            StartsAt.HasValue &&
            PromotionEndsAt.HasValue &&
            DateTime.UtcNow >= StartsAt.Value &&
            DateTime.UtcNow < PromotionEndsAt.Value;

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;

        public Plan Plan { get; set; } = null!;

        public Promotion? Promotion { get; set; }

        public TenantSubscriptionStatus TenantSubscriptionStatus { get; set; } = null!;
    }
}