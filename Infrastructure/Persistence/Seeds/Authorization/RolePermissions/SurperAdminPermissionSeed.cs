using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class SurperAdminPermissionSeed
    {
        public static RolePermission[] Data => [
            // Tenant
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.GET_TENANTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.ACTIVATE_TENANT
            }
        ];
    }
}