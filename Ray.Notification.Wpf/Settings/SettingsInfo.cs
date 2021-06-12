using System;

namespace Ray.Notification.Wpf.Settings
{
    [Serializable]
    public class SettingsInfo
    {
        public int SecondsVisibilityBalloonTime { get; set; }
        public bool ShowBalloonWithNotificationsOpen { get; set; }
    }
}