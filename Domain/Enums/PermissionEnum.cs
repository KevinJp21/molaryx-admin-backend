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

        // Procedures
        GET_PROCEDURES = 8,
        CREATE_PROCEDURE = 9,
        UPDATE_PROCEDURE = 10,
        DELETE_PROCEDURE = 11,

        // Appointments
        GET_APPOINTMENTS = 12,
        CREATE_APPOINTMENT = 13,
        UPDATE_APPOINTMENT = 14,

        // Users / Team
        GET_TEAM = 15,
        CREATE_MEMBER = 16,
        UPDATE_MEMBER = 17,

        // Treatments
        GET_TREATMENTS = 18,
        CREATE_TREATMENT = 19,
        UPDATE_TREATMENT = 20,
        DELETE_TREATMENT = 21,

        // Patient treatments
        CREATE_PATIENT_TREATMENT = 22,
        GET_PATIENT_TREATMENTS = 23,
        UPDATE_PATIENT_TREATMENT = 24,

        // Payments
        CREATE_PAYMENT = 25,
        GET_PAYMENTS = 26,
        GET_PAYMENTS_SUMMARY_BY_CONCEPT = 27,

        // Clinical records
        CREATE_CLINICAL_RECORD = 28,
        GET_CLINICAL_RECORDS = 29,
    }
}