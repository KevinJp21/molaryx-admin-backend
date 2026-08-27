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
            IdPermission = (short)PermissionEnum.GET_TEAM,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.GET_TEAM,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_MEMBER,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.CREATE_MEMBER,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.UPDATE_MEMBER,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.UPDATE_MEMBER,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_PROFILE,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.GET_PROFILE,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}
