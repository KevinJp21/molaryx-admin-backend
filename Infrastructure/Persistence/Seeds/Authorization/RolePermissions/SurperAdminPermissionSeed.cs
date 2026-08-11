using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class SurperAdminPermissionSeed
    {
        public static RolePermission[] Data => [
            // Tenant
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.GET_PF_TENANTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.ACTIVATE_PF_TENANT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.CREATE_PF_BUSINESS_TENANT
            },
        ];
    }
}