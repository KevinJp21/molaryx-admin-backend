using Domain.Common;

namespace Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public short IdUserRole { get; set; }

        public short IdPermission { get; set; }

        public UserRole UserRoles { get; set; } = null!;

        public Permission Permissions { get; set; } = null!;
    }
}