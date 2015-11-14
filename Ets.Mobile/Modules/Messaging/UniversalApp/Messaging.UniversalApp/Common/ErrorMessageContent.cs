using System;
using Messaging.Interfaces.Common;

namespace Messaging.UniversalApp.Common
{
    public sealed class ErrorMessageContent : IExceptionMessagingContent
    {
        public ErrorMessageContent(string message, Exception exception)
        {
            Exception = exception;
            Message = message;
        }

        public ErrorMessageContent(string message, string title, Exception exception)
        {
            Exception = exception;
            Title = title;
            Message = message;
        }

        public Exception Exception { get; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}