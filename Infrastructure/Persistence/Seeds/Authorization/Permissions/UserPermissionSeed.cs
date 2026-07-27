using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class UserPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.USERS_READ,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.USERS_READ,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.USERS_CREATE,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.USERS_CREATE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.USERS_UPDATE,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.USERS_UPDATE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.USERS_DELETE,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.USERS_DELETE,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}