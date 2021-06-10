using Microsoft.AspNet.SignalR.Client;
using Ray.Notification.Client.EventArgs;
using Ray.Notification.Domain;
using System;
using System.Threading.Tasks;

namespace Ray.Notification.Client
{
    public class NotificationHubConnection : IDisposable, INotificationHubConnection
    {
        public HubConnection HubConnection { get; set; }
        public bool IsConnect { set; get; }

        private IHubProxy _proxyHub;

        private const string ProcessNotificationDsc = "ProcessNotification";
        private const string SendNotificationDsc = "SendNotification";
        private const string SendAnnouncementDsc = "SendAnnouncementNotifications";
        private const string SendLetterDsc = "SendLetterNotifications";
        private const string SendMessageDsc = "SendMessageNotifications";
        private const string SendDraftDsc = "SendDraftNotifications";
        private const string DeleteLetterInstanceDsc = "DeleteLetterInstanceNotifications";
        private const string FolderContentChangedDsc = "FolderContentChanged";

        public event EventHandler<NotificationEventArgs> ProcessNotification;

        public NotificationHubConnection(HubConnection hubConnection, IHubProxy proxyHub)
        {
            HubConnection = hubConnection;
            IsConnect = false;

            _proxyHub = proxyHub;

            OnConnect();
        }

        private void OnConnect()
        {
            try
            {
                OnProcessNotification();

                OnAnnounce();

                OnSaveLetter();

                OnMessage();

                OnDraft();

                OnDeleteLetterInstance();

                OnFolderContentChanged();

                IsConnect = true;

                Task.WaitAll(HubConnection.Start());
            }
            catch (Exception ex)
            {
                throw new HubException(
                    $"Error to connect to Service. Check the service is online, and the ServiceAddress is correct. Error:{ex.Message}");
            }
        }

        private void OnProcessNotification()
        {
            _proxyHub.On(ProcessNotificationDsc, (NotificationMessage notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnProcessMessage(notification);
                }
            });
        }

        private void OnFolderContentChanged()
        {
            _proxyHub.On(FolderContentChangedDsc, (string notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnNotify(notification);
                }
            });
        }

        private void OnDeleteLetterInstance()
        {
            _proxyHub.On(DeleteLetterInstanceDsc, (Domain.Notification notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnNotify(notification);
                }
            });
        }

        private void OnDraft()
        {
            _proxyHub.On(SendDraftDsc, (Domain.Notification notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnNotify(notification);
                }
            });
        }

        private void OnMessage()
        {
            _proxyHub.On(SendMessageDsc, (Domain.Notification notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnNotify(notification);
                }
            });
        }

        private void OnSaveLetter()
        {
            _proxyHub.On(SendLetterDsc, (Domain.Notification notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnNotify(notification);
                }
            });
        }

        private void OnAnnounce()
        {
            _proxyHub.On(SendAnnouncementDsc, (Domain.Notification notification) =>
            {
                if (notification != null && HubConnection != null)
                {
                    OnNotify(notification);
                }
            });
        }

        public Task SendNotificationAsync(NotificationMessage notification) =>
            _proxyHub.Invoke(SendNotificationDsc, notification);

        public Task SendAnnouncementNotifications(Domain.Notification notification) => _proxyHub.Invoke(SendAnnouncementDsc, notification);

        public Task SendLetterNotifications(Domain.Notification notification) => _proxyHub.Invoke(SendLetterDsc, notification);

        public Task SendMessageNotifications(Domain.Notification notification) => _proxyHub.Invoke(SendMessageDsc, notification);

        public Task SendDraftNotifications(Domain.Notification notification) => _proxyHub.Invoke(SendDraftDsc, notification);

        public Task DeleteLetterInstanceNotifications(Domain.Notification notification) => _proxyHub.Invoke(DeleteLetterInstanceDsc, notification);

        public Task FolderContentChanged(string notification) => _proxyHub.Invoke(FolderContentChangedDsc, notification);


        protected internal virtual void OnProcessMessage(NotificationMessage notification) =>
            ProcessNotification?.Invoke(this, new NotificationEventArgs(notification));

        protected internal virtual void OnNotify(Domain.Notification notification) =>
            ProcessNotification?.Invoke(this, new NotificationEventArgs(notification));

        protected internal virtual void OnNotify(string notification) =>
            ProcessNotification?.Invoke(this, new NotificationEventArgs(new Domain.Notification { FolderId = Guid.Parse(notification) }));

        public void Dispose()
        {
            IsConnect = false;

            HubConnection.Dispose();

            HubConnection = null;

            _proxyHub = null;
        }
    }
}
