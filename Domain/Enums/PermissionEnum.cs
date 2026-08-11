namespace Domain.Enums
{
    public enum PermissionEnum : short
    {
        /* Tenants */
        GET_PF_TENANTS = 1,
        ACTIVATE_PF_TENANT = 2,
        CREATE_PF_BUSINESS_TENANT = 3,

        /* Patients */
        GET_USER_PATIENTS = 4,
    }
}