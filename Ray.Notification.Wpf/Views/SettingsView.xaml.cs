using Ray.Notification.Wpf.ViewModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace Ray.Notification.Wpf.Views
{

    public partial class SettingsView : Window
    {
        private readonly Storyboard _unloadMovie;

        public SettingsView()
        {
            InitializeComponent();

            _unloadMovie = (Storyboard)Resources["UnLoadAnimation"];

            var viewModel = (SettingsViewModel)DataContext;

            viewModel.SavedSettings += (sender, e) => CloseWithAnimation();

            MouseDown += (sender, e) =>
            {
                DragMove();
                e.Handled = false;
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseWithAnimation();
        }


        private void CloseWithAnimation()
        {
            _unloadMovie.Completed += (sender, e) =>
            {
                Close();
            };

            _unloadMovie.Begin();
        }
    }
}
