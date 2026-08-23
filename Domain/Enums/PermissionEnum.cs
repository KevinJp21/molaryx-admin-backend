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

        // Treatments
        GET_TREATMENTS = 17,
        CREATE_TREATMENT = 18,
        UPDATE_TREATMENT = 19,
        DELETE_TREATMENT = 20,

        // Patient treatments
        CREATE_PATIENT_TREATMENT = 21,
        GET_PATIENT_TREATMENTS = 22,
        UPDATE_PATIENT_TREATMENT = 23,

        // Payments
        CREATE_PAYMENT = 24,
        GET_PAYMENTS = 25,
        GET_PAYMENTS_SUMMARY_BY_CONCEPT = 26,

        // Clinical records
        CREATE_CLINICAL_RECORD = 27,
        GET_CLINICAL_RECORDS = 28,
    }
}