using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class OwnerPermissionSeed
    {
        public static RolePermission[] Data => [
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_CREATE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_READ
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_UPDATE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_DELETE
            },
        ];
    }
}