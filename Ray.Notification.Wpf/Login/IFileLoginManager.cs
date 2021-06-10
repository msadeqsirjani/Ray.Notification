namespace Ray.Notification.Wpf.Login
{
    public interface IFileLoginManager
    {
        bool ExistsLoginFile(string path);
        void WriteLoginFile(string path, LoginInfo loginInfo);
        LoginInfo ReadLoginFile(string path);
    }
}