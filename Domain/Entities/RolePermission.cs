using Domain.Common;

namespace Domain.Entities
{
    public class RolePermission
    {
        public short IdUserRole { get; set; }

        public short IdPermission { get; set; }

        public UserRole UserRole { get; set; } = null!;

        public Permission Permission { get; set; } = null!;
    }
}