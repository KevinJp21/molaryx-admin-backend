using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class AssistantPermissionSeed
    {
        public static RolePermission[] Data => [
            // Patients
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.GET_PATIENTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.CREATE_PATIENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.UPDATE_PATIENT
            },
            // Procedures
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.GET_PROCEDURES
            },
            // Appointments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.GET_APPOINTMENTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.CREATE_APPOINTMENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.UPDATE_APPOINTMENT
            },
            // Team
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.GET_TEAM
            },
            // Treatments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.GET_TREATMENTS
            },
            // Patient treatments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.ASSISTANT,
                IdPermission = (short)PermissionEnum.GET_PATIENT_TREATMENTS
            },
        ];
    }
}
