using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions;

public static class PaymentPermissionSeed
{
    public static Permission[] Data =>
    [
        new Permission
        {
            IdPermission = (short)PermissionEnum.CREATE_PAYMENT,
            IdModule = (short)ModuleEnum.PAYMENTS,
            Code = PermissionCodes.CREATE_PAYMENT,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_PAYMENTS,
            IdModule = (short)ModuleEnum.PAYMENTS,
            Code = PermissionCodes.GET_PAYMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Permission
        {
            IdPermission = (short)PermissionEnum.GET_PAYMENTS_SUMMARY_BY_CONCEPT,
            IdModule = (short)ModuleEnum.PAYMENTS,
            Code = PermissionCodes.GET_PAYMENTS_SUMMARY_BY_CONCEPT,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}
