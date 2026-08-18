using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.Seeds.Authorization.RolePermissions
{
    public static class OwnerPermissionSeed
    {
        public static RolePermission[] Data => [
            // Patients
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
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.DELETE_PATIENT
            },
            // Services
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_SERVICES
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_SERVICE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.UPDATE_SERVICE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.DELETE_SERVICE
            },
            // Appointments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_APPOINTMENTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_APPOINTMENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.UPDATE_APPOINTMENT
            },
            // Users
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_PROFESSIONALS
            },
            // Treatments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_TREATMENTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_TREATMENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.UPDATE_TREATMENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.DELETE_TREATMENT
            },
            // Patient treatments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_PATIENT_TREATMENTS
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_PATIENT_TREATMENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.UPDATE_PATIENT_TREATMENT
            },
            // Payments
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_PAYMENT
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_PAYMENTS
            },
            // Clinical records
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_CLINICAL_RECORD
            }
        ];
    }
}