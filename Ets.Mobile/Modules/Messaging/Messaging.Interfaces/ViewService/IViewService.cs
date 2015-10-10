using Messaging.Interfaces.Notifications;
using Messaging.Interfaces.Popup;

namespace Messaging.Interfaces.ViewService
{
    public interface IViewService
    {
        IPopupManager Popup { get; }
        INotificationManager Notification { get; }
    }
}