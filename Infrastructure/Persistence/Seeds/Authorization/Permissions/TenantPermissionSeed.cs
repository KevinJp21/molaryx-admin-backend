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
            IdPermission = (short)PermissionEnum.GET_PF_TENANTS,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.GET_PF_TENANTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.ACTIVATE_PF_TENANT,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.ACTIVATE_PF_TENANT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_PF_BUSINESS_TENANT,
            IdModule = (short)ModuleEnum.TENANTS,
            Code = PermissionCodes.CREATE_PF_BUSINESS_TENANT,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}