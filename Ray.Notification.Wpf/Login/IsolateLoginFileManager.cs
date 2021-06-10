using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Ray.Notification.Wpf.Login
{
    public class IsolateLoginFileManager : IFileLoginManager
    {
        public bool ExistsLoginFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), $@"The parameter {nameof(path)} can't be null/white/white space");

            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            var result = isolatedStorage.FileExists(path);

            isolatedStorage.Dispose();

            return result;
        }

        public LoginInfo ReadLoginFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), $@"The parameter {nameof(path)} can't be null/white/white space");

            if (!ExistsLoginFile(path))
                throw new InvalidOperationException($"No exists a IsolateStorage settings file {path} in your IsolateStorage");

            var result = new LoginInfo();

            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            using (var isoStream = new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorage))
            {
                using (var reader = new StreamReader(isoStream))
                {
                    var fileContent = reader.ReadToEnd();

                    var parts = fileContent.Split(';');

                    result.Username = parts[0].Replace("\r", "").Replace("\n", "");
                    result.Password = parts[1].Replace("\r", "").Replace("\n", "");
                }
            }

            isolatedStorage.Dispose();

            return result;
        }

        public void WriteLoginFile(string path, LoginInfo loginInfo)
        {
            if (loginInfo == null)
                throw new ArgumentNullException(nameof(loginInfo), $@"The parameter {nameof(loginInfo)} can't be null");

            if (ExistsLoginFile(path))
            {
                WriteExistsFile(path, loginInfo);
            }
            else
            {
                WriteNewFile(path, loginInfo);
            }
        }

        private static void WriteNewFile(string path, LoginInfo loginInfo)
        {
            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            using (var isoStream = new IsolatedStorageFileStream(path, FileMode.CreateNew, isolatedStorage))
            {
                using (var writer = new StreamWriter(isoStream))
                {
                    writer.WriteLine($"{loginInfo.Username};{loginInfo.Password}");
                }
            }

            isolatedStorage.Dispose();
        }

        private static void WriteExistsFile(string path, LoginInfo loginInfo)
        {
            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            isolatedStorage.DeleteFile(path);

            isolatedStorage.Dispose();

            WriteNewFile(path, loginInfo);
        }
    }
}