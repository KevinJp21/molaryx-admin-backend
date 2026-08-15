using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class AppointmentPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_APPOINTMENTS,
            IdModule = (short)ModuleEnum.APPOINTMENTS,
            Code = PermissionCodes.GET_APPOINTMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_APPOINTMENT,
            IdModule = (short)ModuleEnum.APPOINTMENTS,
            Code = PermissionCodes.CREATE_APPOINTMENT,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}