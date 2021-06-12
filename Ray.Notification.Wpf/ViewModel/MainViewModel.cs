using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ray.Notification.Wpf.Views;
using System.Windows;

namespace Ray.Notification.Wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private readonly NotificationView _viewNotifications;
        public NotificationViewModel NotificationViewModel { get; set; }

        public MainViewModel()
        {
            _viewNotifications = new NotificationView();
            NotificationViewModel = _viewNotifications.DataContext as NotificationViewModel;

            GetNotificationsCountRegister();
        }

        private int _notificationsCount;
        public int NotificationsCount
        {
            get => _notificationsCount;
            set => Set(nameof(NotificationsCount), ref _notificationsCount, value);
        }

        public RelayCommand OpenNotificationsCommand => new RelayCommand(OpenNotifications);

        public RelayCommand OpenSettingsViewCommand => new RelayCommand(() => new SettingsView().ShowDialog());

        private void OpenNotifications()
        {
            if (_viewNotifications.Visibility != Visibility.Visible)
            {
                _viewNotifications.ViewNotifications();
            }
        }

        private void GetNotificationsCountRegister()
        {
            NotificationViewModel.Notifications.CollectionChanged += (sender, e) => NotificationsCount = (int)NotificationViewModel.Notifications?.Count;
        }
    }
}