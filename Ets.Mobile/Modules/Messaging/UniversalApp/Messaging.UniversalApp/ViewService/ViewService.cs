using Messaging.Interfaces.Notifications;
using Messaging.Interfaces.Popup;
using Messaging.Interfaces.ViewService;
using Splat;

namespace Messaging.UniversalApp.ViewService
{
    public sealed class ViewService : IViewService
    {
        public ViewService()
        {
            _serviceLocator = Locator.CurrentMutable;
        }

        private readonly IMutableDependencyResolver _serviceLocator;

        private IPopupManager _pm;
        public IPopupManager Popup => _pm ?? (_pm = _serviceLocator.GetService<IPopupManager>());

        private INotificationManager _nm;
        public INotificationManager Notification => _nm ?? (_nm = _serviceLocator.GetService<INotificationManager>());
    }
}
