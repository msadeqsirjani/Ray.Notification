using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Ray.Notification.Wpf.Settings
{
    public class IsolateSettingsFileManager : IFileSettingsManager
    {
        public bool ExistsSettingsFile(string fileSettingsPath)
        {
            if (string.IsNullOrWhiteSpace(fileSettingsPath))
                throw new ArgumentNullException(nameof(fileSettingsPath), $@"The parameter {nameof(fileSettingsPath)} can't be null/white/white space");

            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            var result = isolatedStorage.FileExists(fileSettingsPath);

            isolatedStorage.Dispose();

            return result;
        }

        public SettingsInfo ReadSettingsFile(string fileSettingsPath)
        {
            if (string.IsNullOrWhiteSpace(fileSettingsPath))
                throw new ArgumentNullException(nameof(fileSettingsPath), $@"The parameter {nameof(fileSettingsPath)} can't be null/white/white space");

            if (!ExistsSettingsFile(fileSettingsPath))
                throw new InvalidOperationException($"No exists a IsolateStorage settings file {fileSettingsPath} in your IsolateStorage");

            var result = new SettingsInfo();

            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            using (var isoStream = new IsolatedStorageFileStream(fileSettingsPath, FileMode.Open, isolatedStorage))
            {
                using (var reader = new StreamReader(isoStream))
                {
                    var fileContent = reader.ReadToEnd();

                    var parts = fileContent.Split(';');

                    result.ServiceAddress = parts[0];
                    result.SecondsVisibilityBalloonTime = int.Parse(parts[1]);
                    result.ShowBalloonWithNotificationsOpen = bool.Parse(parts[2]);
                }
            }

            isolatedStorage.Dispose();

            return result;
        }

        public void WriteSettingsFile(string fileSettingsPath, SettingsInfo settingsInfo)
        {
            if (settingsInfo == null)
                throw new ArgumentNullException(nameof(settingsInfo), $@"The parameter {nameof(settingsInfo)} can't be null");

            if (ExistsSettingsFile(fileSettingsPath))
            {
                WriteExistsFile(fileSettingsPath, settingsInfo);
            }
            else
            {
                WriteNewFile(fileSettingsPath, settingsInfo);
            }
        }

        private static void WriteNewFile(string fileSettingsPath, SettingsInfo settingsInfo)
        {
            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            using (var isoStream = new IsolatedStorageFileStream(fileSettingsPath, FileMode.CreateNew, isolatedStorage))
            {
                using (var writer = new StreamWriter(isoStream))
                {
                    writer.WriteLine($"{settingsInfo.ServiceAddress};{settingsInfo.SecondsVisibilityBalloonTime};{settingsInfo.ShowBalloonWithNotificationsOpen}");
                }
            }

            isolatedStorage.Dispose();
        }

        private static void WriteExistsFile(string fileSettingsPath, SettingsInfo settingsInfo)
        {
            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            isolatedStorage.DeleteFile(fileSettingsPath);

            isolatedStorage.Dispose();


            WriteNewFile(fileSettingsPath, settingsInfo);
        }
    }
}