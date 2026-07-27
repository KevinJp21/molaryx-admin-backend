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
            Code = ModuleCodes.Tenants,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.USERS,
            Code = ModuleCodes.Users,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.PATIENTS,
            Code = ModuleCodes.Patients,
            CreatedAt = SeedConstants.SeedDate
        }
    ];
}