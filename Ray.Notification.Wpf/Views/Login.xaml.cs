using Ray.Notification.Common.Services.WebConnector;
using System.Windows;
using System.Windows.Input;

namespace Ray.Notification.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IWebConnectorService _webConnectorService;

        public Login(IWebConnectorService webConnectorService)
        {
            _webConnectorService = webConnectorService;

            InitializeComponent();


            MouseDown += (sender, e) =>
            {
                DragMove();
                e.Handled = false;
            };
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            var username = TxtUsername.Text;
            var password = TxtPassword.Password;

            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            //{
            //    MessageBox.Show("در وارد کردن نام کاربری و رکز عبور دقت نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //try
            //{
            //    var token = _loginService.GetAuthenticationToken(username, password);
            //    var isValid = token != null;

            //    if (!isValid) throw new ArgumentException("نام کاربری یا رمز عبور اشتباه می باشد");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, " خطا", MessageBoxButton.OK,
            //        MessageBoxImage.Warning);
            //    return;
            //}

            //LoginBuilder.BuildLogin().WriteLogin(new LoginInfo { Username = username, Password = password });
        }

        private void Border_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
