namespace Domain.Enums
{
    public enum PermissionEnum : short
    {
        TENANTS_READ = 1,
        TENANTS_CREATE = 2,
        TENANTS_UPDATE = 3,
        TENANTS_DELETE = 4,

        USERS_READ = 5,
        USERS_CREATE = 6,
        USERS_UPDATE = 7,
        USERS_DELETE = 8
    }
}