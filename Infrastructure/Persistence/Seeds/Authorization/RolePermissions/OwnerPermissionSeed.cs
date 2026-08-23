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
            // Procedures
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_PROCEDURES
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_PROCEDURE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.UPDATE_PROCEDURE
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.DELETE_PROCEDURE
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
                IdPermission = (short)PermissionEnum.GET_TEAM
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
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_PAYMENTS_SUMMARY_BY_CONCEPT
            },
            // Clinical records
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.CREATE_CLINICAL_RECORD
            },
            new RolePermission{
                IdUserRole = (short)UserRoleEnum.OWNER,
                IdPermission = (short)PermissionEnum.GET_CLINICAL_RECORDS
            }
        ];
    }
}