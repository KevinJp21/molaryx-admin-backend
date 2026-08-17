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
        }
    ];
}
