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
                IdPermission = (short)PermissionEnum.USERS_CREATE,
                CreatedAt = SeedConstants.SeedDate
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_READ,
                CreatedAt = SeedConstants.SeedDate
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_UPDATE,
                CreatedAt = SeedConstants.SeedDate
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.USERS_DELETE,
                CreatedAt = SeedConstants.SeedDate
            },
        ];
    }
}