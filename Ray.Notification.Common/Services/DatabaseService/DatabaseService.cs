using System.Collections.Generic;

namespace Ray.Notification.Common.Services.DatabaseService
{
    public class DatabaseService
    {
        public DatabaseService()
        {
            Store = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Store { get; }
    }
}
