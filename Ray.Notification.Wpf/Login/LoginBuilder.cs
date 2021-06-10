using System.IO.IsolatedStorage;

namespace Ray.Notification.Wpf.Login
{
    public class LoginBuilder
    {
        public static ILoginManager BuildLogin()
        {
            var isolatedStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            IFileLoginManager fileLoginManager = new IsolateLoginFileManager();

            ILoginManager result = new LoginManager(fileLoginManager);

            return result;
        }
    }
}