namespace Ray.Notification.Wpf.Settings
{
    public interface ISettingsManager
    {
        SettingsInfo ReadSettings();
        void WriteSettings(SettingsInfo settingsInfo);
    }
}