namespace Domain.Enums
{
    public enum TenantSubscriptionStatusEnum : short
    {
        PENDING = 1,
        ACTIVE = 2,
        SCHEDULED = 3,
        CANCELLED = 4,
        EXPIRED = 5,
        SUSPENDED = 6
    }
}