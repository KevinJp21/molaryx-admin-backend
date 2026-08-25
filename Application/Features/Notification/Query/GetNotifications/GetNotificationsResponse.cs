namespace Application.Features.Notification.Query.GetNotifications
{
    public class GetNotificationsResponse
    {
        public long IdNotification { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsViewed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
