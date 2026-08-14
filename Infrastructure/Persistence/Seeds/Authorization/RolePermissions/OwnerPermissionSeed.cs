using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class OwnerPermissionSeed
    {
        public static RolePermission[] Data => [
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_PATIENTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_PATIENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.UPDATE_PATIENT
            }
        ];
    }
}