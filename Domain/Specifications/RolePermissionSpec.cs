using Domain.Common;
using Domain.Entities;

namespace Domain.Specifications
{
    public class RolePermissionSpec : BaseSpecification<RolePermission>
    {
        public static RolePermissionSpec ByRoleAndCode(short idUserRole, string permissionCode)
        {
            var spec = new RolePermissionSpec
            {
                Criteria = rp =>
                    rp.IdUserRole == idUserRole
                    && rp.Permission.Code == permissionCode
            };
            return spec;
        }

        private RolePermissionSpec()
        {
        }
    }
}
