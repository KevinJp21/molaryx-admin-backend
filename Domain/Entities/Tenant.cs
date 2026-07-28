using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_tenant_type")]
        public short IdTenantType { get; set; }

        [Column("id_tenant_status")]
        public short IdTenantStatus { get; set; }

        [Column("consultory_name")]
        public string ConsultoryName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("cell_phone")]
        public string CellPhone { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        // Navigation properties
        public TenantType TenantType { get; set; } = null!;
        public TenantStatus TenantStatus { get; set; } = null!;

        public ICollection<TenantSubscription> TenantSubscriptions { get; set; } = [];
        public ICollection<User> Users { get; set; } = [];
    }
}