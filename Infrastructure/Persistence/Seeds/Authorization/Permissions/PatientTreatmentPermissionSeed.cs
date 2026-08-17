using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class PatientTreatmentPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_PATIENT_TREATMENTS,
            IdModule = (short)ModuleEnum.PATIENT_TREATMENTS,
            Code = PermissionCodes.GET_PATIENT_TREATMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_PATIENT_TREATMENT,
            IdModule = (short)ModuleEnum.PATIENT_TREATMENTS,
            Code = PermissionCodes.CREATE_PATIENT_TREATMENT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.UPDATE_PATIENT_TREATMENT,
            IdModule = (short)ModuleEnum.PATIENT_TREATMENTS,
            Code = PermissionCodes.UPDATE_PATIENT_TREATMENT,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}
