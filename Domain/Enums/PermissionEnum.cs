namespace Domain.Enums
{
    public enum PermissionEnum : short
    {
        // Tenants
        GET_PF_TENANTS = 1,
        ACTIVATE_PF_TENANT = 2,
        CREATE_PF_BUSINESS_TENANT = 3,

        // Patients
        GET_PATIENTS = 4,
        CREATE_PATIENT = 5,
        UPDATE_PATIENT = 6,
        DELETE_PATIENT = 7,

        // Services
        GET_SERVICES = 8,
        CREATE_SERVICE = 9,
        UPDATE_SERVICE = 10,
        DELETE_SERVICE = 11,

        // Appointments
        GET_APPOINTMENTS = 12,
        CREATE_APPOINTMENT = 13,
        UPDATE_APPOINTMENT = 15,

        // Users
        GET_PROFESSIONALS = 14,

        // Treatments
        CREATE_TREATMENT = 16,
    }
}