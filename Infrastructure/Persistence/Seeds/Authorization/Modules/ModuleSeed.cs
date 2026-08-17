using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Modules;

public static class ModuleSeed
{
    public static Module[] Data =>
    [
        new Module
        {
            IdModule = (short)ModuleEnum.TENANTS,
            Code = ModuleCodes.TENANTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.USERS,
            Code = ModuleCodes.USERS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.PATIENTS,
            Code = ModuleCodes.PATIENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.SERVICES,
            Code = ModuleCodes.SERVICES,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.APPOINTMENTS,
            Code = ModuleCodes.APPOINTMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.TREATMENTS,
            Code = ModuleCodes.TREATMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
    ];
}