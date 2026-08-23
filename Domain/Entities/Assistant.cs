using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Assistant : BaseEntity
    {
        [Column("id_assistant")]
        public long IdAssistant { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_user")]
        public long IdUser { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}