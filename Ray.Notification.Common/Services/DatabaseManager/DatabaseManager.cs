using System;
using System.Collections.Generic;

namespace Ray.Notification.Common.Services.DatabaseManager
{
    public class DatabaseManager
    {
        private DatabaseManager()
        {
            Store = new Dictionary<string, string>();
        }

        private static readonly Lazy<DatabaseManager> Database = new Lazy<DatabaseManager>(() => new DatabaseManager());
        public static DatabaseManager Instance => Database.Value;
        public Dictionary<string, string> Store { get; }
    }
}
