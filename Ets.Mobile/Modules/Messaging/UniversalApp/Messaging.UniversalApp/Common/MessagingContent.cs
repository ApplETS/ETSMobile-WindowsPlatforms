using Messaging.Interfaces.Common;

namespace Messaging.UniversalApp.Common
{
    public class MessagingContent : IMessagingContent
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}