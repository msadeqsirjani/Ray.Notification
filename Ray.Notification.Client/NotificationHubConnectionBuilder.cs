using System;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;

namespace Ray.Notification.Client
{
    public class NotificationHubConnectionBuilder
    {
        private const string Hub = "shellNoti";

        public static NotificationHubConnection CreateConnection(string url, string token, bool useDefaultUrl = true)
        {
            var queryString = new Dictionary<string, string>
            {
                {"Authorization", token}
            };

            var hubConnection = new HubConnection(url, queryString, useDefaultUrl);
            var hubProxy = hubConnection.CreateHubProxy(Hub);

            try
            {
                return new NotificationHubConnection(hubConnection, hubProxy);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
