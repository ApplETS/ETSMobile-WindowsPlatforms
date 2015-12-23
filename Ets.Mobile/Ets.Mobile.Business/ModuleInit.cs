using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.ApplicationModel;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.DesignTime;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;
using Ets.Mobile.Entities.ServiceInfo;
using Ets.Mobile.Entities.Signets;
using Moduler;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Refit;
using Splat;

namespace Ets.Mobile.Business
{
	public class ModuleInit : IModuleInitializer
	{
		public void Initialize(IMutableDependencyResolver container)
		{
            container.RegisterLazySingleton(() => new SignetsAccountVm(), typeof(SignetsAccountVm));

            if (DesignMode.DesignModeEnabled)
            {
                container.RegisterLazySingleton(() => new DtSignetsBusinessService(), typeof(ISignetsBusinessService));
            }
            else
            {
                container.RegisterLazySingleton(() =>
                    new SignetsServiceInfo { Url = "https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx" },
                    typeof(IClientInfo)
                );

                // NetCache.UserInitiated
                var client = new HttpClient(container.GetService<HttpMessageHandler>())
                {
                    BaseAddress = new Uri(container.GetService<IClientInfo>().Url),
                };

                var refitSettings = new RefitSettings
                {
                    JsonSerializerSettings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> {new DecimalConverter(), new DoubleConverter()}
                    }
                };

                container.RegisterLazySingleton(() => RestService.For<ISignetsBusinessService>(client, refitSettings), typeof(ISignetsBusinessService));
            }
		}
	}

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-ddThh:mm:ss";
        }
    }
}
