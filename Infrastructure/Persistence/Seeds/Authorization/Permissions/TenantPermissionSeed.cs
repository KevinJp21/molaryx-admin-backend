using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class TenantPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.TENANTS_READ,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.TENANTS_READ,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.TENANTS_CREATE,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.TENANTS_CREATE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.TENANTS_UPDATE,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.TENANTS_UPDATE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.TENANTS_DELETE,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.TENANTS_DELETE,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}