using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PromotionPlan
    {
        [Column("id_promotion")]
        public long IdPromotion { get; set; }

        [Column("id_plan")]
        public short IdPlan { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        // Navigation properties
        public Promotion Promotion { get; set; } = null!;

        public Plan Plan { get; set; } = null!;
    }
}