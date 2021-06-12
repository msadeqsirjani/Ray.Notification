using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Ray.Notification.Client;
using Ray.Notification.Common.Services.DatabaseManager;
using Ray.Notification.Common.Services.WebConnector;
using Ray.Notification.Common.Utils;
using Ray.Notification.Wpf.Settings;

namespace Ray.Notification.Wpf.ViewModel
{
    public class ViewModelLocator
    {
        private readonly DatabaseManager _databaseManager;
        private readonly IWebConnectorService _webConnectorService;

        public ViewModelLocator()
        {
            _databaseManager = DatabaseManager.Instance;
            _webConnectorService = new WebConnectorService();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NotificationViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();


        public NotificationViewModel Notification
        {
            get
            {
                try
                {
                    var url = _webConnectorService.GetBaseUrl();

                    var token = _databaseManager.Store.GetOrDefaultValue(DatabaseConstant.Authorization);
                    if (string.IsNullOrEmpty(token))
                    {
                        token = _webConnectorService.Get(DatabaseConstant.Authorization, false).Value.ToString();
                        _databaseManager.Store.Add(DatabaseConstant.Authorization, token);
                    }

                    var connectHub = NotificationHubConnectionBuilder.CreateConnection(url, token);

                    return connectHub != null
                        ? new NotificationViewModel(connectHub)
                        : new NotificationViewModel(null) { IsConnected = false };
                }
                catch
                {
                    return new NotificationViewModel(null) { IsConnected = false };
                }
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                var settingsManager = SettingsBuilder.BuildSettings();

                return new SettingsViewModel(settingsManager, _databaseManager, _webConnectorService);
            }
        }
    }
}