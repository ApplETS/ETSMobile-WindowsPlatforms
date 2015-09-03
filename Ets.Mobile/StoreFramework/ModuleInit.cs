using Windows.ApplicationModel.Resources;
using CrittercismSDK;
using Splat;
using StoreFramework.Composite;
using StoreFramework.Logger;
using StoreFramework.Messaging;
using StoreFramework.Messaging.Notifications;
using StoreFramework.Messaging.Popup;

namespace Module.StoreFramework
{
	public class ModuleInit : IModuleInitializer
	{
		public void Initialize(IMutableDependencyResolver container)
		{
            container.Register(() => new ResourceLoader(), typeof(ResourceLoader));
            container.Register(() => new PopupManager(container.GetService<ResourceLoader>()), typeof(IPopupManager));
            container.Register(() => new InAppNotificationManager(), typeof(INotificationManager), "InApp");
            container.Register(() => new ViewService(), typeof(IViewService));
        }
	}
}
