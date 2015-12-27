using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.DesignTime;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations;
using Ets.Mobile.Client.Services;
using Ets.Mobile.Entities.ServiceInfo;
using Ets.Mobile.Entities.Signets;
using Logger;
using Logger.CrittercismLog;
using Messaging.Interfaces.Notifications;
using Messaging.Interfaces.Popup;
using Messaging.Interfaces.ViewService;
using Messaging.UniversalApp.Notifications;
using Messaging.UniversalApp.Popup;
using Messaging.UniversalApp.ViewService;
using ModernHttpClient;
using Newtonsoft.Json;
using Refit;
using Splat;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Themes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;

namespace Ets.Mobile.Shared
{
    public static class ModuleInit
	{
		public static void Initialize(IMutableDependencyResolver resolver)
		{
            if (resolver == null)
			{
				throw new ArgumentNullException(nameof(resolver));
			}

            // Set up Fusillade
            //
            // Fusillade is a super cool library that will make it so that whenever
            // we issue web requests, we'll only issue 4 concurrently, and if we
            // end up issuing multiple requests to the same resource, it will
            // de-dupe them. We're saying here, that we want our *backing*
            // HttpMessageHandler to be ModernHttpClient.
            resolver.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));

            // Log 
            resolver.RegisterLazySingleton(() => new CrittercismLogger(), typeof(IUserEnabledLogger));

            // View Services
            resolver.RegisterLazySingleton(() => new PopupManager(resolver.GetService<ResourceLoader>()), typeof(IPopupManager));
            resolver.RegisterLazySingleton(() => new InAppNotificationManager(AppBrushes.MediumBrush), typeof(INotificationManager), "InApp");
            resolver.RegisterLazySingleton(() => new ViewService(), typeof(IViewService));

            // Business Services
            resolver.RegisterLazySingleton(() => new SignetsAccountVm(), typeof(SignetsAccountVm));

            if (DesignMode.DesignModeEnabled)
            {
                resolver.RegisterLazySingleton(() => new DtSignetsBusinessService(), typeof(ISignetsBusinessService));
            }
            else
            {
                resolver.RegisterLazySingleton(() =>
                    new SignetsServiceInfo { Url = "https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx" },
                    typeof(IClientInfo)
                );

                // NetCache.UserInitiated
                var client = new HttpClient(resolver.GetService<HttpMessageHandler>())
                {
                    BaseAddress = new Uri(resolver.GetService<IClientInfo>().Url),
                };

                var refitSettings = new RefitSettings
                {
                    JsonSerializerSettings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new DecimalConverter(), new DoubleConverter() }
                    }
                };

                resolver.RegisterLazySingleton(() => RestService.For<ISignetsBusinessService>(client, refitSettings), typeof(ISignetsBusinessService));
            }

            // Client Services
            resolver.RegisterLazySingleton(() => new SignetsFactory(), typeof(SignetsAbstractFactory));
            var signetServiceInstance = new SignetsService(
                resolver.GetService<ISignetsBusinessService>(),
                resolver.GetService<SignetsAbstractFactory>()
            );
            resolver.RegisterLazySingleton(() => signetServiceInstance, typeof(ISignetsService));
            resolver.RegisterLazySingleton(() => new CustomSettingsService(), typeof(ICustomSettingsService));
        }
	}
}