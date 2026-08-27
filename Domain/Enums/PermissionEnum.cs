namespace Domain.Enums
{
    public enum PermissionEnum : short
    {
        // Tenants
        GET_PF_TENANTS = 1,
        ACTIVATE_PF_TENANT = 2,
        CREATE_PF_BUSINESS_TENANT = 3,
        UPDATE_PF_TENANT = 4,

        // Patients
        GET_PATIENTS = 5,
        CREATE_PATIENT = 6,
        UPDATE_PATIENT = 7,
        DELETE_PATIENT = 8,

        // Procedures
        GET_PROCEDURES = 9,
        CREATE_PROCEDURE = 10,
        UPDATE_PROCEDURE = 11,
        DELETE_PROCEDURE = 12,

        // Appointments
        GET_APPOINTMENTS = 13,
        CREATE_APPOINTMENT = 14,
        UPDATE_APPOINTMENT = 15,

        // Users / Team
        GET_TEAM = 16,
        CREATE_MEMBER = 17,
        UPDATE_MEMBER = 18,
        GET_PROFILE = 19,

        // Treatments
        GET_TREATMENTS = 20,
        CREATE_TREATMENT = 21,
        UPDATE_TREATMENT = 22,
        DELETE_TREATMENT = 23,

        // Patient treatments
        CREATE_PATIENT_TREATMENT = 24,
        GET_PATIENT_TREATMENTS = 25,
        UPDATE_PATIENT_TREATMENT = 26,

        // Payments
        CREATE_PAYMENT = 27,
        GET_PAYMENTS = 28,
        GET_PAYMENTS_SUMMARY_BY_CONCEPT = 29,

        // Clinical records
        CREATE_CLINICAL_RECORD = 30,
        GET_CLINICAL_RECORDS = 31,
    }
}
