using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class TenantType : BaseEntity
    {
        [Column("id_tenant_type")]
        public short IdTenantType { get; set; }

        [Column("code")]
        public string Code { get; set; } = string.Empty;

        public ICollection<Promotion> Promotions { get; set; } = [];
    }
}