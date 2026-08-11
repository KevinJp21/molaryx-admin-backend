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
            IdPermission = (short)PermissionEnum.GET_USER_PATIENTS,
            IdModule = (short)ModuleEnum.PATIENTS,
            Code = PermissionCodes.GET_USER_PATIENTS,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}