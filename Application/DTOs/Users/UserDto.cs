namespace Application.DTOs.Users
{
    public class UserDto
    {
        public long IdUser { get; set; }
        public UserRole Role { get; set; } = null!;
        public long? IdTenant { get; set; }
        public UserStatus Status { get; set; } = null!;
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
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
}