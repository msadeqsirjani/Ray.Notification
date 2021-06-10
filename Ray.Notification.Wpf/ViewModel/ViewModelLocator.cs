using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Ray.Notification.Client;
using Ray.Notification.Common.Services.LoginService;
using Ray.Notification.Wpf.Login;
using Ray.Notification.Wpf.Settings;
using System;
using System.Windows;

namespace Ray.Notification.Wpf.ViewModel
{
    public class ViewModelLocator
    {
        private readonly LoginService _loginService;

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NotificationViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();

            _loginService = new LoginService();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();


        public NotificationViewModel Notification
        {
            get
            {
                try
                {
                    var serviceAddress = SettingsBuilder.BuildSettings().ReadSettings().ServiceAddress;

                    var loginInfo = LoginBuilder.BuildLogin().ReadLogin();

                    if (loginInfo == null)
                        return new NotificationViewModel(null);

                    var token = _loginService.GetAuthenticationToken(loginInfo.Username, loginInfo.Password);

                    if (token == null)
                        throw new ArgumentException("با عرض پوزش خطایی به وجود آمده است");

                    var connectHub = NotificationHubConnectionBuilder.CreateConnection(serviceAddress, token.AccessToken);

                    if (connectHub != null) return new NotificationViewModel(connectHub);

                    throw new ArgumentException("با عرض پوزش خطایی به وجود آمده است");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                var settingsManager = SettingsBuilder.BuildSettings();

                return new SettingsViewModel(settingsManager);
            }
        }
    }
}