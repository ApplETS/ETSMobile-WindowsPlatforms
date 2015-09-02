using StoreFramework.Messaging.Common;

namespace StoreFramework.Messaging.Notifications
{
    public interface INotificationManager
    {
        void Notify(IMessagingContent messageContent);
    }
}
