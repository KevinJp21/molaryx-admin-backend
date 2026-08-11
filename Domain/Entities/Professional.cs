using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Professional : BaseEntity
    {
        [Column("id_professional")]
        public long IdProfessional { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_user")]
        public long IdUser { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<ProfessionalSchedule> ProfessionalSchedules { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}