using System;
using Messaging.Interfaces.Common;

namespace Messaging.UniversalApp.Common
{
    public sealed class ErrorMessageContent : Exception, IExceptionMessagingContent
    {
        public ErrorMessageContent(string message, Exception exception) : base(message)
        {
            Exception = exception;
        }

        public ErrorMessageContent(string message, string title, Exception exception) : base(message, exception)
        {
            Exception = exception;
            Title = title;
        }

        public Exception Exception { get; }
        public string Title { get; set; }
    }
}