namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQueryResponse
    {
        public UserRole Role { get; set; } = null!;
        public long? IdTenant { get; set; }
        public UserStatus Status { get; set; } = null!;
        public string Username { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string Surnames { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SubscriptionSummary? Subscription { get; set; }
        public List<ModulePermissions> Permissions { get; set; } = [];
    }

    public class UserRole
    {
        public short IdUserRole { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UserStatus
    {
        public short IdUserStatus { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SubscriptionSummary
    {
        public string PlanName { get; set; } = string.Empty;
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public int? DaysRemaining { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }

    public class ModulePermissions
    {
        public string Module { get; set; } = string.Empty;
        public List<string> Codes { get; set; } = [];
    }
}
