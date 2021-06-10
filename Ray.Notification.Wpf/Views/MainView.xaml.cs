using Ray.Notification.Wpf.ViewModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace Ray.Notification.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly NotificationViewModel _notificationViewModel;
        private readonly Storyboard _tryToolTipImageAnimation;

        public MainView()
        {
            InitializeComponent();

            var mainViewModel = DataContext as MainViewModel;

            _notificationViewModel = mainViewModel?.NotificationViewModel;
            _tryToolTipImageAnimation = (Storyboard)Resources["TryToolTipImageAnimation"];

            SuscriveToHideAddNotifications();
        }

        private void SuscriveToHideAddNotifications()
        {
            if (_notificationViewModel == null)
                return;

            _notificationViewModel.Notification += (sender, e) =>
            {
                var balloon = new Balloon { DataContext = e.Notification };

                var milliseconds = Settings.SettingsBuilder.BuildSettings().ReadSettings().SecondsVisibilityBalloonTime * 1000;

                tbTaskBarIcon.ShowCustomBalloon(balloon, System.Windows.Controls.Primitives.PopupAnimation.Slide, milliseconds);
            };
        }

        private void TaskBarIcon_TrayToolTipOpen(object sender, RoutedEventArgs e)
        {
            _tryToolTipImageAnimation.Begin();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
