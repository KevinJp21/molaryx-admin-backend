using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class PatientsPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_PATIENTS,
            IdModule = (short)ModuleEnum.PATIENTS,
            Code = PermissionCodes.GET_PATIENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_PATIENT,
            IdModule = (short)ModuleEnum.PATIENTS,
            Code = PermissionCodes.CREATE_PATIENT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.UPDATE_PATIENT,
            IdModule = (short)ModuleEnum.PATIENTS,
            Code = PermissionCodes.UPDATE_PATIENT,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}