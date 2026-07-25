namespace Application.Features.Auth.Query.GetSessions
{
    public class GetSessionsItemResponse
    {
        public long IdUserSession { get; set; }
        public string? Device { get; set; }
        public string IpConnection { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool IsCurrent { get; set; }

    }
}