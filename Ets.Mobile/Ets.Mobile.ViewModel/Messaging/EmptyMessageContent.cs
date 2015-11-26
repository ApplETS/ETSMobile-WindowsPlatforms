using System;
using Messaging.Interfaces.Common;

namespace Ets.Mobile.ViewModel.Messaging
{
    public class EmptyMessageContent : Exception, IMessagingContent
    {
        public EmptyMessageContent(string title, string message) : base(message)
        {
            Title = title;
        }

        public string Title { get; }
    }
}