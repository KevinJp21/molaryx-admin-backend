using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class TenantStatus : BaseEntity
    {
        [Column("id_tenant_status")]
        public short IdTenantStatus { get; set; }
        [Column("code")]
        public short Code { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Tenant> Tenants { get; set; } = [];
    }
}