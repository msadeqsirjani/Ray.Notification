using System;

namespace Ray.Notification.Common.Exceptions
{
    public class WebConnectorException : Exception
    {
        public WebConnectorException()
        {

        }

        public WebConnectorException(string message) : base(message)
        {

        }
    }
}