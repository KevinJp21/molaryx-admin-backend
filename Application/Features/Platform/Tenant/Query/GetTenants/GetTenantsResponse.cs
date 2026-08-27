namespace Application.Features.Platform.Tenant.Query.GetTenants
{
    public class GetTenantsResponse
    {
        public long IdTenant { get; set; }

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

        public GetTenantsOwnerResponse? Owner { get; set; }

        public GetTenantsSubscriptionResponse? Subscription { get; set; }
    }

    public class GetTenantsOwnerResponse
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

    public class GetTenantsSubscriptionResponse
    {
        public long IdTenantSubscription { get; set; }

        public long IdTenant { get; set; }

        public short IdTenantSubscriptionStatus { get; set; }

        public string StatusName { get; set; } = string.Empty;

        public short IdPlan { get; set; }

        public string PlanName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public short? MaxProfessionals { get; set; }

        public short? MaxAssistants { get; set; }

        public int? MaxPatients { get; set; }

        public DateTime? StartsAt { get; set; }

        public DateTime? EndsAt { get; set; }

        public int? DaysRemaining { get; set; }

        public DateTime? PromotionEndsAt { get; set; }

        public bool IsPromotionActive { get; set; }
    }
}
