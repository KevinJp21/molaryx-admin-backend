namespace Application.DTOs.Sessions
{
    public class UserSessionDto
    {
        public long IdUserSession { get; set; }
        public string? Device { get; set; }
        public string IpConnection { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsCurrent { get; set; }
    }
}