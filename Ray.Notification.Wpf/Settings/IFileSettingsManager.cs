namespace Ray.Notification.Wpf.Settings
{
    public interface IFileSettingsManager
    {
        bool ExistsSettingsFile(string path);
        void WriteSettingsFile(string path, SettingsInfo settingsInfo);
        SettingsInfo ReadSettingsFile(string path);
    }
}