using System;

namespace Ray.Notification.Domain
{
    [Serializable]
    public class NotificationMessage 
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime MessageDate { get; set; } = DateTime.Now;
        public string UriImage { get; set; }
    }
}