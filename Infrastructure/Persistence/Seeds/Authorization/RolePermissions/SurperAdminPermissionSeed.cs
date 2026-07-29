using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class SurperAdminPermissionSeed
    {
        public static RolePermission[] Data => [

            // User
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.USERS_CREATE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.USERS_READ
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.USERS_UPDATE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.USERS_DELETE
            },

            // Tenant

            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.TENANTS_CREATE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.TENANTS_READ
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.TENANTS_UPDATE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.SUPER_ADMIN,
                IdPermission = (short)PermissionEnum.TENANTS_DELETE
            },
        ];
    }
}