namespace Ray.Notification.Wpf.Login
{
    public interface ILoginManager
    {
        LoginInfo ReadLogin();
        void WriteLogin(LoginInfo loginInfo);
    }
}