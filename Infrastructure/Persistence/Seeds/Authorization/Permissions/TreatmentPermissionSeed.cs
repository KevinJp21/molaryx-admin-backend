using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class TreatmentPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_TREATMENT,
            IdModule = (short)ModuleEnum.TREATMENTS,
            Code = PermissionCodes.CREATE_TREATMENT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.UPDATE_TREATMENT,
            IdModule = (short)ModuleEnum.TREATMENTS,
            Code = PermissionCodes.UPDATE_TREATMENT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_TREATMENTS,
            IdModule = (short)ModuleEnum.TREATMENTS,
            Code = PermissionCodes.GET_TREATMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.DELETE_TREATMENT,
            IdModule = (short)ModuleEnum.TREATMENTS,
            Code = PermissionCodes.DELETE_TREATMENT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_PATIENT_TREATMENT,
            IdModule = (short)ModuleEnum.TREATMENTS,
            Code = PermissionCodes.CREATE_PATIENT_TREATMENT,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}
