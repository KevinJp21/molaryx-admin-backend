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
            IdModule = (short)ModuleEnum.PROCEDURES,
            Code = ModuleCodes.PROCEDURES,
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
        new Module
        {
            IdModule = (short)ModuleEnum.PATIENT_TREATMENTS,
            Code = ModuleCodes.PATIENT_TREATMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.PAYMENTS,
            Code = ModuleCodes.PAYMENTS,
            CreatedAt = SeedConstants.SeedDate
        },
        new Module
        {
            IdModule = (short)ModuleEnum.CLINICAL_RECORDS,
            Code = ModuleCodes.CLINICAL_RECORDS,
            CreatedAt = SeedConstants.SeedDate
        },
    ];
}