using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class RolePermission
    {
        [Column("id_user_role")]
        public short IdUserRole { get; set; }

        [Column("id_permission")]
        public short IdPermission { get; set; }

        public UserRole UserRole { get; set; } = null!;

        public Permission Permission { get; set; } = null!;
    }
}