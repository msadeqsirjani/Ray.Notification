using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Controls;

namespace Ray.Notification.Wpf.Views
{
    public partial class Balloon : UserControl
    {
        public Balloon()
        {
            InitializeComponent();
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            var icon = TaskbarIcon.GetParentTaskbarIcon(this);

            icon.CloseBalloon();
        }
    }
}
