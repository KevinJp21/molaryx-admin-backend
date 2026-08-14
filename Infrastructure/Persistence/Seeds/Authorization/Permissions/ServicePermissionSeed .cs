using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class ServicePermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_SERVICES,
            IdModule = (short)ModuleEnum.SERVICES,
            Code = PermissionCodes.GET_SERVICES,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_SERVICE,
            IdModule = (short)ModuleEnum.SERVICES,
            Code = PermissionCodes.CREATE_SERVICE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.UPDATE_SERVICE,
            IdModule = (short)ModuleEnum.SERVICES,
            Code = PermissionCodes.UPDATE_SERVICE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.DELETE_SERVICE,
            IdModule = (short)ModuleEnum.SERVICES,
            Code = PermissionCodes.DELETE_SERVICE,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}