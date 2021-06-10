using Ray.Notification.Wpf.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Ray.Notification.Wpf.Views
{
    public partial class NotificationView : Window
    {
        private readonly Storyboard _unloadMovie;
        private readonly Storyboard _loadMovie;
        private readonly Storyboard _unClearAllAnimations;
        private readonly NotificationViewModel _viewModel;

        public NotificationView()
        {
            InitializeComponent();

            Width = 400;
            Height = SystemParameters.WorkArea.Height;

            var desktopWorkingArea = SystemParameters.WorkArea;

            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;

            _unloadMovie = (Storyboard)Resources["UnLoadAnimation"];
            _loadMovie = (Storyboard)Resources["LoadAnimation"];
            _unClearAllAnimations = (Storyboard)Resources["UnClearAllAnimation"];

            _viewModel = DataContext as NotificationViewModel;

            AddListBoxScrollEnding();
            DesSelectedListBoxItems();
        }

        private void DesSelectedListBoxItems()
        {
            lstMensajes.SelectionChanged += (sender, e) => lstMensajes.SelectedIndex = -1;
        }

        private void AddListBoxScrollEnding()
        {
            if(_viewModel == null)
                return;

            _viewModel.Notifications.CollectionChanged += (sender, e) =>
            {
                if (!_viewModel.Notifications.Any() || lstMensajes.Items.Count <= 0) return;
                var lastItem = lstMensajes.Items[lstMensajes.Items.Count - 1];
                lstMensajes.ScrollIntoView(lastItem);
            };
        }
        public void ViewNotifications()
        {
            _viewModel.IsViewOpen = true;

            _loadMovie.Begin();
        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }

        private void Window_Deactivated(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _unloadMovie.Completed += (sender2, e2) =>
            {
                Visibility = Visibility.Collapsed;
                _viewModel.IsViewOpen = false;
            };

            _unloadMovie.Begin();
        }

        private void Bd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private async void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var notification = ((FrameworkElement)e.OriginalSource).DataContext as Domain.Notification;

            await Task.Delay(500);

            _viewModel.DeletedNotification(notification);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {

        }

        private void ClearAllAnimation_Completed(object sender, EventArgs e)
        {
            _viewModel.DeletedAllNotification();

            _unClearAllAnimations.Begin();
        }
    }
}
