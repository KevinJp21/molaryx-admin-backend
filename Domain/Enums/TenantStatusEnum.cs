namespace Domain.Enums
{
    public enum TenantStatusEnum : short
    {
        ACTIVE = 1,
        INACTIVE = 2,
        PENDING_APPROVAL = 3,
        BLOCKED = 4,
        REJECTED = 5
    }
}