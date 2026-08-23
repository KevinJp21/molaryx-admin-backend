using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Procedure : BaseEntity
    {
        [Column("id_procedure")]
        public long IdProcedure { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("reference_price")]
        public decimal? ReferencePrice { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public ICollection<AppointmentProcedure> AppointmentProcedures { get; set; } = [];
        public ICollection<ClinicalRecord> ClinicalRecords { get; set; } = [];
    }
}
