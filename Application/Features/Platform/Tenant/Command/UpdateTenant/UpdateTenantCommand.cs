using Application.Common.Mediator.Interfaces;

namespace Application.Features.Platform.Tenant.Command.UpdateTenant
{
    public class UpdateTenantCommand : IRequest<bool>
    {
        public long IdTenant { get; set; }
        public UpdateTenantInfoRequest? Tenant { get; set; }
        public UpdateTenantOwnerRequest? Owner { get; set; }
        public UpdateTenantSubscriptionRequest? Subscription { get; set; }
    }

    public class UpdateTenantInfoRequest
    {
        public short? IdIdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? ConsultoryName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public short? IdTenantStatus { get; set; }
    }

    public class UpdateTenantOwnerRequest
    {
        public long IdUser { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? FirstSurname { get; set; }
        public string? SecondSurname { get; set; }
        public short? IdIdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public short? IdUserStatus { get; set; }
    }

    public class UpdateTenantSubscriptionRequest
    {
        public long IdTenantSubscription { get; set; }
        public short? IdTenantSubscriptionStatus { get; set; }
        public decimal? Price { get; set; }
        public short? MaxProfessionals { get; set; }
        public short? MaxAssistants { get; set; }
        public int? MaxPatients { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
    }
}
