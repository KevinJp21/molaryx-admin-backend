namespace Application.DTOs.Tenant.TenantRegistration
{
    public class TenantRegistrationDto
    {
        public short? IdIdentificationType { get; set; }

        public string? IdentificationNumber { get; set; }

        public string ConsultoryName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CellPhone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}