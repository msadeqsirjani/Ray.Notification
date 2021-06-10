using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Ray.Notification.Client;
using Ray.Notification.Client.EventArgs;
using Ray.Notification.Wpf.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Ray.Notification.Wpf.ViewModel
{
    public class NotificationViewModel : ViewModelBase
    {
        private readonly INotificationHubConnection _hubConnection;

        public event EventHandler<NotificationEventArgs> Notification;

        private ObservableCollection<Domain.Notification> _notifications;
        public ObservableCollection<Domain.Notification> Notifications
        {
            get => _notifications;
            set => Set(nameof(Domain.Notification), ref _notifications, value);
        }

        private bool _isViewOpen;
        public bool IsViewOpen
        {
            get => _isViewOpen;
            set => Set(nameof(IsViewOpen), ref _isViewOpen, value);
        }

        private bool _isConnected = true;
        public bool IsConnected
        {
            get => _isConnected;
            set => Set(nameof(IsConnected), ref _isConnected, value);
        }

        public RelayCommand OpenSettingsViewCommand => new RelayCommand(() => new SettingsView().ShowDialog());

        public void DeletedNotification(Domain.Notification item)
        {
            if (item == null) return;
            Notifications.Remove(item);
        }

        public void DeletedAllNotification()
        {
            Notifications?.Clear();
        }

        public NotificationViewModel(INotificationHubConnection hubConnection)
        {
            Notifications = new ObservableCollection<Domain.Notification>();

            _hubConnection = hubConnection;

            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            if (_hubConnection == null)
                return;

            _hubConnection.ProcessNotification += (sender, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Notifications.Add(e.Notification);

                    var showBalloon = Settings.SettingsBuilder.BuildSettings().ReadSettings().ShowBalloonWithNotificationsOpen;

                    if (showBalloon)
                    {
                        OnNotification(e.Notification);
                    }
                    else
                    {
                        if (!IsViewOpen) OnNotification(e.Notification);
                    }
                });

            };
        }

        protected virtual void OnNotification(Domain.Notification notification) => Notification?.Invoke(this, new NotificationEventArgs(notification));
    }
}
