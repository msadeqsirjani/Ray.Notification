using System.IO.IsolatedStorage;

namespace Ray.Notification.Wpf.Settings
{
    public class SettingsBuilder
    {
        public static ISettingsManager BuildSettings()
        {
            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            IFileSettingsManager fileSettingsManager = new IsolateSettingsFileManager();

            ISettingsManager result = new SettingsManager(fileSettingsManager);

            return result;
        }
    }
}
