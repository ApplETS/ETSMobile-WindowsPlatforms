using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations.Moodle;
using Ets.Mobile.Client.Factories.Implementations.Signets;
using Ets.Mobile.Client.Services;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Logger;
using Logger;
using Logger.CrittercismLog;
using Logger.SplatLog;
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
		    var splatLogger = new SplatLogger();
		    
            resolver.RegisterLazySingleton(() => splatLogger, typeof(IFullLogger));
            resolver.RegisterLazySingleton(() => splatLogger, typeof(ILogger));
            resolver.RegisterLazySingleton(() => new SplatLogManager(splatLogger), typeof(ILogManager));

            var logger = new CombinedLogger(new CrittercismLogger(), splatLogger);
            resolver.RegisterLazySingleton(() => logger, typeof(IUserEnabledLogger));
            
            

            // View Services
            resolver.RegisterLazySingleton(() => new PopupManager(resolver.GetService<ResourceLoader>()), typeof(IPopupManager));
            resolver.RegisterLazySingleton(() => new InAppNotificationManager(AppBrushes.MediumBrush), typeof(INotificationManager), "InApp");
            resolver.RegisterLazySingleton(() => new ViewService(), typeof(IViewService));

            // Business Services
		    InitializeBusinessServices(resolver);

            // Client Services
            InitializeClientServices(resolver);

            // Custom Settings
            resolver.RegisterLazySingleton(() => new CustomSettingsService(), typeof(ICustomSettingsService));
        }

        private static void InitializeBusinessServices(IMutableDependencyResolver resolver)
        {
            // Signets Services
            //
            resolver.RegisterLazySingleton(() => new EtsUserCredentials(), typeof(EtsUserCredentials));
            
            resolver.RegisterLazySingleton(() =>
                new SignetsClientInfo { Url = "https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx" },
                typeof(SignetsClientInfo)
            );
            
            var clientSignets = new HttpClient(resolver.GetService<HttpMessageHandler>())
            {
                BaseAddress = new Uri(resolver.GetService<SignetsClientInfo>().Url),
            };

            var refitSettingsSignets = new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new DecimalConverter(), new DoubleConverter() }
                }
            };

            resolver.RegisterLazySingleton(() => RestService.For<ISignetsBusinessService>(clientSignets, refitSettingsSignets), typeof(ISignetsBusinessService));

            // Moodle Services
            //
            resolver.RegisterLazySingleton(() =>
                new MoodleClientInfo { Url = "https://ena.etsmtl.ca/" },
                typeof(MoodleClientInfo)
            );
            
            var clientMoodle = new HttpClient(resolver.GetService<HttpMessageHandler>())
            {
                BaseAddress = new Uri(resolver.GetService<MoodleClientInfo>().Url)
            };


            resolver.RegisterLazySingleton(() => RestService.For<IMoodleBusinessService>(clientMoodle), typeof(IMoodleBusinessService));

            resolver.RegisterLazySingleton(() => new MoodleService(resolver.GetService<IMoodleBusinessService>(), new MoodleFactory()), typeof(IMoodleService));

            // SSO Service
            //
            resolver.RegisterLazySingleton(() => new SsoService(resolver.GetService<ISignetsService>(), resolver.GetService<IMoodleService>()), typeof(ISsoService));
        }

        private static void InitializeClientServices(IMutableDependencyResolver resolver)
        {
            // Signets Services
            //
            resolver.RegisterLazySingleton(() => new SignetsFactory(), typeof(SignetsAbstractFactory));
            var signetServiceInstance = new SignetsService(
                resolver.GetService<ISignetsBusinessService>(),
                resolver.GetService<SignetsAbstractFactory>()
            );
            resolver.RegisterLazySingleton(() => signetServiceInstance, typeof(ISignetsService));

            // Moodle Services
            //
            resolver.RegisterLazySingleton(() => new MoodleFactory(), typeof(MoodleAbstractFactory));
            var moodleServiceInstance = new MoodleService(
                resolver.GetService<IMoodleBusinessService>(),
                resolver.GetService<MoodleAbstractFactory>()
            );
            resolver.RegisterLazySingleton(() => signetServiceInstance, typeof(ISignetsService));
        }

    }
}