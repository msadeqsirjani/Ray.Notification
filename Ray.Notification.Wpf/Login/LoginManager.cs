using System;

namespace Ray.Notification.Wpf.Login
{
    public class LoginManager : ILoginManager
    {
        private readonly IFileLoginManager _fileLoginManager;

        public LoginManager(IFileLoginManager fileLoginManager)
        {
            _fileLoginManager = fileLoginManager;
        }

        public LoginInfo ReadLogin()
        {
            if (!_fileLoginManager.ExistsLoginFile(LoginGlobalData.PathFileLoginName)) return null;

            var result = _fileLoginManager.ReadLoginFile(LoginGlobalData.PathFileLoginName);

            return result;

        }

        public void WriteLogin(LoginInfo loginInfo)
        {
            if (loginInfo == null)
                throw new ArgumentNullException(nameof(loginInfo), $@"The parameter {nameof(loginInfo)} can't be null");

            _fileLoginManager.WriteLoginFile(LoginGlobalData.PathFileLoginName, loginInfo);
        }
    }
}