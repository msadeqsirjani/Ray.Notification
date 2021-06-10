using Newtonsoft.Json;

namespace Ray.Notification.Domain
{
    public class ModifiedNotification
    {
        public Notification Notification { get; set; }
        [JsonProperty("isSearchMode")]
        public bool IsSearchMode { get; set; }
    }
}