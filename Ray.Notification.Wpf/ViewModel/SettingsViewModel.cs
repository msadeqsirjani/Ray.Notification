using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Ray.Notification.Client;
using Ray.Notification.Common.Services.DatabaseManager;
using Ray.Notification.Common.Services.WebConnector;
using Ray.Notification.Common.Utils;
using Ray.Notification.Wpf.Settings;
using Ray.Notification.Wpf.Views;
using System;
using System.Linq;
using System.Windows;

namespace Ray.Notification.Wpf.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly DatabaseManager _databaseManager;
        private readonly ISettingsManager _settingsManager;
        private readonly IWebConnectorService _webConnectorService;

        public event EventHandler SavedSettings;

        public SettingsViewModel(ISettingsManager settingsManager, DatabaseManager databaseManager, IWebConnectorService webConnectorService)
        {
            _settingsManager = settingsManager;
            _databaseManager = databaseManager;
            _webConnectorService = webConnectorService;

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
                var url = _webConnectorService.GetBaseUrl();

                var notificationView = Application.Current.Windows.OfType<NotificationView>().FirstOrDefault();

                if (notificationView == null) return;

                var token = _databaseManager.Store.GetOrDefaultValue(DatabaseConstant.Authorization);
                if (string.IsNullOrEmpty(token))
                {
                    token = _webConnectorService.Get(DatabaseConstant.Authorization, false).Value.ToString();
                    _databaseManager.Store.Add(DatabaseConstant.Authorization, token);
                }

                var connectHub = NotificationHubConnectionBuilder.CreateConnection(url, token);

                if (connectHub == null)
                {
                    notificationView.DataContext = new NotificationViewModel(null) { IsConnected = false };
                }
                else
                {
                    notificationView.DataContext = new NotificationViewModel(connectHub) { IsConnected = true };

                    var notificationViewModel = (NotificationViewModel)notificationView.DataContext;

                    notificationViewModel.DeletedAllNotification();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            Model = _settingsManager.ReadSettings();

            SecondsVisibilityBalloonTime = Model.SecondsVisibilityBalloonTime;
            ShowBalloonWithNotificationsOpen = Model.ShowBalloonWithNotificationsOpen;
        }

        protected virtual void OnSavedSettings() => SavedSettings?.Invoke(this, new EventArgs());
    }
}
