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
        public const string CREATE_APPOINTMENT = "CREATE_APPOINTMENT";

    }
}