using System;
using Windows.ApplicationModel.Resources;
using Logger;
using Logger.CrittercismLog;
using Messaging.Interfaces.Notifications;
using Messaging.Interfaces.Popup;
using Messaging.Interfaces.ViewService;
using Messaging.UniversalApp.Notifications;
using Messaging.UniversalApp.Popup;
using Messaging.UniversalApp.ViewService;
using Moduler;
using Splat;
using Themes;

namespace Ets.Mobile.Shared
{
	public class ModuleInit : IModuleInitializer
	{
		private IMutableDependencyResolver _resolver;
		
		public void Initialize(IMutableDependencyResolver resolver)
		{
            if (resolver == null)
			{
				throw new ArgumentNullException(nameof(resolver));
			}
			_resolver = resolver;

            // Log 
            _resolver.Register(() => new CrittercismLogger(), typeof(IUserEnabledLogger));
            _resolver.Register(() => new PopupManager(_resolver.GetService<ResourceLoader>()), typeof(IPopupManager));
            _resolver.Register(() => new InAppNotificationManager(AppBrushes.MediumBrush), typeof(INotificationManager), "InApp");
            _resolver.Register(() => new ViewService(), typeof(IViewService));

            var types = new[]
			{
                typeof (Entities.ModuleInit),
				typeof (Business.ModuleInit),
				typeof (Client.ModuleInit),
				typeof (ViewModel.ModuleInit)
			};

			foreach (var type in types)
			{
				InnerInitialize(type);
			}
		}

		private void InnerInitialize(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			var module = Activator.CreateInstance(type) as IModuleInitializer;

			if (module == null)
			{
				throw new InvalidCastException(nameof(module));
			}

			module.Initialize(_resolver);
		}
	}
}
