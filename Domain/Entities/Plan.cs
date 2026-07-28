using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        [Column("id_plan")]
        public short IdPlan { get; set; }

        [Column("code")]
        public string Code { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("max_professionals")]
        public short? MaxProfessionals { get; set; }

        [Column("max_assistants")]
        public short? MaxAssistants { get; set; }

        [Column("max_patients")]
        public int? MaxPatients { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<TenantSubscription> TenantSubscriptions { get; set; } = [];
    }
}