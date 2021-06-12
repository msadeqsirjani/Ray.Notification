using System.Collections.Generic;

namespace Ray.Notification.Common.Utils
{
    public static class DictionaryExtensions
    {
        public static string GetOrDefaultValue(this Dictionary<string, string> @this, string key)
        {
            return @this.ContainsKey(key) ? @this[key] : string.Empty;
        }
    }
}
