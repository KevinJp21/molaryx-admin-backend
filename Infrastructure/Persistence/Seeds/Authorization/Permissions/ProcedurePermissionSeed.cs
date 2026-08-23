using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class ProcedurePermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_PROCEDURES,
            IdModule = (short)ModuleEnum.PROCEDURES,
            Code = PermissionCodes.GET_PROCEDURES,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_PROCEDURE,
            IdModule = (short)ModuleEnum.PROCEDURES,
            Code = PermissionCodes.CREATE_PROCEDURE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.UPDATE_PROCEDURE,
            IdModule = (short)ModuleEnum.PROCEDURES,
            Code = PermissionCodes.UPDATE_PROCEDURE,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.DELETE_PROCEDURE,
            IdModule = (short)ModuleEnum.PROCEDURES,
            Code = PermissionCodes.DELETE_PROCEDURE,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}
