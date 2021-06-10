using System;

namespace Ray.Notification.Wpf.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly IFileSettingsManager _fileSettingsManager;

        public SettingsManager(IFileSettingsManager fileSettingsManager)
        {
            _fileSettingsManager = fileSettingsManager;
        }

        public SettingsInfo ReadSettings()
        {
            SettingsInfo result;

            if (_fileSettingsManager.ExistsSettingsFile(SettingsGlobalData.PathFileSettingsName))
            {
                result = _fileSettingsManager.ReadSettingsFile(SettingsGlobalData.PathFileSettingsName);
            }
            else
            {
                result = new SettingsInfo { ServiceAddress = "http://localhost:11111", SecondsVisibilityBalloonTime = 4, ShowBalloonWithNotificationsOpen = false };

                _fileSettingsManager.WriteSettingsFile(SettingsGlobalData.PathFileSettingsName, result);
            }

            return result;
        }

        public void WriteSettings(SettingsInfo settingsInfo)
        {
            if (settingsInfo == null)
                throw new ArgumentNullException(nameof(settingsInfo), $@"The parameter {nameof(settingsInfo)} can't be null");

            _fileSettingsManager.WriteSettingsFile(SettingsGlobalData.PathFileSettingsName, settingsInfo);
        }
    }
}