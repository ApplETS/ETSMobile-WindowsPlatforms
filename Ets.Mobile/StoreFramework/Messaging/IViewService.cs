using StoreFramework.Messaging.Notifications;
using StoreFramework.Messaging.Popup;

namespace StoreFramework.Messaging
{
    public interface IViewService
    {
        IPopupManager Popup { get; }
        INotificationManager Notification { get; }
    }
}
