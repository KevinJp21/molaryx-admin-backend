namespace Application.Common.Interfaces
{
    public interface ITenantRegistration
    {
        short? IdIdentificationType { get; }
        string? IdentificationNumber { get; }
        string ConsultoryName { get; }
        string Email { get; }
        string PhoneNumber { get; }
        string Address { get; }
    }
}