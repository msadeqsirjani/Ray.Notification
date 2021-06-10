using Ray.Notification.Client.EventArgs;
using Ray.Notification.Domain;
using System;
using System.Threading.Tasks;

namespace Ray.Notification.Client
{
    public interface INotificationHubConnection
    {
        event EventHandler<NotificationEventArgs> ProcessNotification;

        void Dispose();
        Task SendNotificationAsync(NotificationMessage notification);
        Task SendAnnouncementNotifications(Domain.Notification notification);
        Task SendLetterNotifications(Domain.Notification notification);
        Task SendMessageNotifications(Domain.Notification notification);
        Task SendDraftNotifications(Domain.Notification notification);
        Task DeleteLetterInstanceNotifications(Domain.Notification notification);
        Task FolderContentChanged(string notification);
    }
}