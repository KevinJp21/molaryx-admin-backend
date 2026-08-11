using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class TenantStatus : BaseEntity
    {
        [Column("id_tenant_status")]
        public short IdTenantStatus { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}