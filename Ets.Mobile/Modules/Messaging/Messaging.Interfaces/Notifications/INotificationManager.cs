using Messaging.Interfaces.Common;

namespace Messaging.Interfaces.Notifications
{
    public interface INotificationManager
    {
        void Notify(IMessagingContent messageContent);
    }
}