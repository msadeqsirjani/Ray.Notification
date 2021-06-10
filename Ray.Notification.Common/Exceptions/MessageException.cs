using System;

namespace Ray.Notification.Common.Exceptions
{
    public class MessageException : Exception
    {
        public MessageException()
        {

        }

        public MessageException(string message) : base(message)
        {

        }
    }
}
