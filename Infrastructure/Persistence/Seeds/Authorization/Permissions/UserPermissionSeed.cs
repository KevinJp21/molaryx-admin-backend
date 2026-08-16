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
            IdPermission = (short)PermissionEnum.GET_PROFESSIONALS,
            IdModule = (short)ModuleEnum.USERS,
            Code = PermissionCodes.GET_PROFESSIONALS,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}