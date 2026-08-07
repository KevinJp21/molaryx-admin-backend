namespace Application.Common.Regex
{
    public static class RegexCatalog
    {
        public const string USERNAME = @"^[a-zA-Z][a-zA-Z0-9._-]{2,29}$";

        public const string NAME = @"^[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:\s+[A-Za-zÁÉÍÓÚáéíóúÑñ]+)*$";

        public const string EMAIL = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public const string PASSWORD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.])[A-Za-z\d@$!%*?&.]{8,}$";

        public const string NIT = @"^\d{10}$";

        public const string IDENTIFICATION_NUMBER = @"^\d{6,10}$";

        public const string PHONE_NUMBER = @"^3\d{9}$";
    }
}