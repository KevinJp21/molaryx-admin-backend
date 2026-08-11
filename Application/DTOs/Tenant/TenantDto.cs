namespace Application.DTOs.Tenant
{
    public class TenantDto
    {
        public long IdTenant { get; set; }

        public long? IdTenantSubscription { get; set; }

        public short? IdIdentificationType { get; set; }

        public string? IdentificationNumber { get; set; }

        public string IdentificationCode { get; set; } = string.Empty;

        public string ConsultoryName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public short IdTenantType { get; set; }

        public string TenantTypeCode { get; set; } = string.Empty;

        public short IdTenantStatus { get; set; }

        public string TenantStatusName { get; set; } = string.Empty;

        public OwnerDto? Owner { get; set; } = null!;
    }

    public class OwnerDto
    {
        public long IdUser { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public short IdIdentificationType { get; set; }

        public string IdentificationCode { get; set; } = string.Empty;

        public string IdentificationNumber { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}