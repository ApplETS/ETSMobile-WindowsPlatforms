using System;

namespace StoreFramework.Messaging.Common
{
    public class ErrorMessageContent : Exception
    {
        public ErrorMessageContent(string message, Exception exception) : base(exception.Message)
        {
            Content = new MessagingContent
            {
                Message = message
            };
        }

        public ErrorMessageContent(string message, string title, Exception exception)
            : base(exception.Message)
        {
            Content = new MessagingContent
            {
                Title = title,
                Message = message
            };
        }

        public readonly IMessagingContent Content;
    }
}
