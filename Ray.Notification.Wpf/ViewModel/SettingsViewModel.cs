using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Ray.Notification.Client;
using Ray.Notification.Common.Services.LoginService;
using Ray.Notification.Wpf.Login;
using Ray.Notification.Wpf.Settings;
using Ray.Notification.Wpf.Views;
using System;
using System.Linq;
using System.Windows;

namespace Ray.Notification.Wpf.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {

        private readonly ISettingsManager _settingsManager;
        private readonly LoginService _loginService;

        public event EventHandler SavedSettings;

        public SettingsViewModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _loginService = new LoginService();

            LoadData();
        }

        private SettingsInfo _model;
        public SettingsInfo Model
        {
            get => _model;
            set => Set(nameof(Model), ref _model, value);
        }

        private string _serviceAddress;
        public string ServiceAddress
        {
            get => _serviceAddress;
            set => Set(nameof(ServiceAddress), ref _serviceAddress, value);
        }

        private int _secondsVisibilityBalloonTime;
        public int SecondsVisibilityBalloonTime
        {
            get => _secondsVisibilityBalloonTime;
            set => Set(nameof(SecondsVisibilityBalloonTime), ref _secondsVisibilityBalloonTime, value);
        }

        private bool _showBalloonWithNotificationsOpen;
        public bool ShowBalloonWithNotificationsOpen
        {
            get => _showBalloonWithNotificationsOpen;
            set => Set(nameof(ShowBalloonWithNotificationsOpen), ref _showBalloonWithNotificationsOpen, value);
        }

        public RelayCommand SaveSettingsCommand => new RelayCommand(SaveSettings);

        private void SaveSettings()
        {
            var settingsInfo = new SettingsInfo
            {
                ServiceAddress = ServiceAddress,
                SecondsVisibilityBalloonTime = SecondsVisibilityBalloonTime,
                ShowBalloonWithNotificationsOpen = ShowBalloonWithNotificationsOpen
            };

            _settingsManager.WriteSettings(settingsInfo);

            Reconnect(ServiceAddress);

            OnSavedSettings();
        }

        private void Reconnect(string serviceAddress)
        {
            try
            {
                var notificationView = Application.Current.Windows.OfType<NotificationView>().FirstOrDefault();

                if (notificationView == null) return;

                var loginInfo = LoginBuilder.BuildLogin().ReadLogin();

                if (loginInfo == null)
                    throw new ArgumentException("لطفا ابتدا وارد حساب کاربری خود شوید");

                var token = _loginService.GetAuthenticationToken(loginInfo.Username, loginInfo.Password);

                if (token == null)
                    throw new ArgumentException("با عرض پوزش خطایی به وجود آمده است");

                var connectHub = NotificationHubConnectionBuilder.CreateConnection(serviceAddress, token.AccessToken);

                if (connectHub == null)
                    throw new ArgumentException("با عرض پوزش خطایی به وجود آمده است");

                notificationView.DataContext = new NotificationViewModel(connectHub) { IsConnected = true };

                var notificationViewModel = (NotificationViewModel)notificationView.DataContext;

                notificationViewModel.DeletedAllNotification();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            Model = _settingsManager.ReadSettings();

            ServiceAddress = Model.ServiceAddress;
            SecondsVisibilityBalloonTime = Model.SecondsVisibilityBalloonTime;
            ShowBalloonWithNotificationsOpen = Model.ShowBalloonWithNotificationsOpen;
        }

        protected virtual void OnSavedSettings() => SavedSettings?.Invoke(this, new EventArgs());
    }
}
