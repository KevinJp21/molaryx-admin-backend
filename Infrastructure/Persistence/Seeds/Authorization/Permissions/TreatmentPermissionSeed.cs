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
        }
    ];
}
