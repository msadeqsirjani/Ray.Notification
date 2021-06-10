using Ray.Notification.Domain;

namespace Ray.Notification.Client.EventArgs
{
    public class NotificationEventArgs
    {
        public NotificationMessage NotificationMessage { get; }
        public Domain.Notification Notification { get; }

        public NotificationEventArgs(NotificationMessage notificationMessage)
        {
            NotificationMessage = notificationMessage;
        }

        public NotificationEventArgs(Domain.Notification notification)
        {
            Notification = notification;
        }
    }
}
