namespace Ray.Notification.Common.Services.WebConnector
{
    public interface IWebConnectorService
    {
        bool IsUserLogin();
        string GetBaseUrl();
        ResultModel Get(string key, bool delete = true);
        bool Set(string key, string value);
        ResultModel GetAllData();
        string GetUserId();
    }
}