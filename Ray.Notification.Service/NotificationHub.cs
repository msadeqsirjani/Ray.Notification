using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ray.Notification.Domain;
using System.Threading.Tasks;

namespace Ray.Notification.Service
{
    [HubName("shellNoti")]
    [HubAuthorize]
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
            var context = Context;
            return base.OnConnected();
        }

        public async Task SendNotification(NotificationMessage message)
        {
            await Clients.All.ProcessNotification(message);
        }
        public async Task SendAnnouncementNotifications(Domain.Notification message)
        {
            await Clients.All.SendAnnouncementNotifications(message);
        }
        public async Task SendLetterNotifications(Domain.Notification message)
        {
            await Clients.All.SendLetterNotifications(message);
        }

        public async Task SendMessageNotifications(Domain.Notification message)
        {
            await Clients.All.SendMessageNotifications(message);
        }

        public async Task SendDraftNotifications(Domain.Notification message)
        {
            await Clients.All.SendDraftNotifications(message);
        }

        public async Task DeleteLetterInstanceNotifications(Domain.Notification message)
        {
            await Clients.All.DeleteLetterInstanceNotifications(message);
        }

        public async Task FolderContentChanged(string message)
        {
            await Clients.All.FolderContentChanged(message);
        }
    }
}
