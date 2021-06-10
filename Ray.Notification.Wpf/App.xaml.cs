using Ray.Notification.Wpf.ViewModel;
using Ray.Notification.Wpf.Views;
using System.Linq;
using System.Windows;

namespace Ray.Notification.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);

            var notificationViewModel = (NotificationViewModel)Current.Windows.OfType<NotificationView>().FirstOrDefault()?.DataContext;

            notificationViewModel.IsConnected = false;

            e.Handled = true;
        }
    }
}
