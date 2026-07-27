using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Permission : BaseEntity
    {
        [Column("id_permission")]
        public short IdPermission { get; set; }
        [Column("id_module")]
        public short IdModule { get; set; }
        [Column("code")]
        public string Code { get; set; } = string.Empty;

        public Module Module { get; set; } = null!;
        public ICollection<RolePermission> RolePermission { get; set; } = [];
    }
}