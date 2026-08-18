namespace Domain.Constants
{
    public static class PermissionCodes
    {

        //Tenants
        public const string GET_PF_TENANTS = "GET_PF_TENANTS";
        public const string ACTIVATE_PF_TENANT = "ACTIVATE_PF_TENANT";
        public const string CREATE_PF_BUSINESS_TENANT = "CREATE_PF_BUSINESS_TENANT";

        // Patients
        public const string GET_PATIENTS = "GET_PATIENTS";
        public const string CREATE_PATIENT = "CREATE_PATIENT";
        public const string UPDATE_PATIENT = "UPDATE_PATIENT";
        public const string DELETE_PATIENT = "DELETE_PATIENT";

        // Services
        public const string GET_SERVICES = "GET_SERVICES";
        public const string CREATE_SERVICE = "CREATE_SERVICE";
        public const string UPDATE_SERVICE = "UPDATE_SERVICE";
        public const string DELETE_SERVICE = "DELETE_SERVICE";

        // Appointments
        public const string GET_APPOINTMENTS = "GET_APPOINTMENTS";
        public const string CREATE_APPOINTMENT = "CREATE_APPOINTMENT";
        public const string UPDATE_APPOINTMENT = "UPDATE_APPOINTMENT";

        // Users
        public const string GET_PROFESSIONALS = "GET_PROFESSIONALS";

        // Treatments
        public const string GET_TREATMENTS = "GET_TREATMENTS";
        public const string CREATE_TREATMENT = "CREATE_TREATMENT";
        public const string UPDATE_TREATMENT = "UPDATE_TREATMENT";
        public const string DELETE_TREATMENT = "DELETE_TREATMENT";

        // Patient treatments
        public const string CREATE_PATIENT_TREATMENT = "CREATE_PATIENT_TREATMENT";
        public const string GET_PATIENT_TREATMENTS = "GET_PATIENT_TREATMENTS";
        public const string UPDATE_PATIENT_TREATMENT = "UPDATE_PATIENT_TREATMENT";

        // Payments
        public const string CREATE_PAYMENT = "CREATE_PAYMENT";
        public const string GET_PAYMENTS = "GET_PAYMENTS";
        public const string GET_PAYMENTS_SUMMARY_BY_CONCEPT = "GET_PAYMENTS_SUMMARY_BY_CONCEPT";

        // Clinical records
        public const string CREATE_CLINICAL_RECORD = "CREATE_CLINICAL_RECORD";
    }
}