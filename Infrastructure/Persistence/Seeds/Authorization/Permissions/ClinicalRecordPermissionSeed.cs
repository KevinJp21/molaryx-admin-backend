using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;

namespace Infrastructure.Persistence.Seeds.Authorization.Permissions
{
    public static class ClinicalRecordPermissionSeed
    {
        public static Permission[] Data =>
        [
            new Permission
            {
                IdPermission = (short)PermissionEnum.CREATE_CLINICAL_RECORD,
                IdModule = (short)ModuleEnum.CLINICAL_RECORDS,
                Code = PermissionCodes.CREATE_CLINICAL_RECORD,
                CreatedAt = SeedConstants.SeedDate
            },
            new Permission
            {
                IdPermission = (short)PermissionEnum.GET_CLINICAL_RECORDS,
                IdModule = (short)ModuleEnum.CLINICAL_RECORDS,
                Code = PermissionCodes.GET_CLINICAL_RECORDS,
                CreatedAt = SeedConstants.SeedDate
            }
        ];
    }
}