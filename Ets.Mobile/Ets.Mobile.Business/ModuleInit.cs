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
using Refit;
using Splat;

namespace Ets.Mobile.Business
{
	public class ModuleInit : IModuleInitializer
	{
		public void Initialize(IMutableDependencyResolver container)
		{
            container.Register(() => new SignetsAccountVm(), typeof(SignetsAccountVm));

            if (DesignMode.DesignModeEnabled)
            {
                container.Register(() => new DtSignetsBusinessService(), typeof(ISignetsBusinessService));
            }
            else
            {
                container.Register(() =>
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

                container.Register(() => RestService.For<ISignetsBusinessService>(client, refitSettings), typeof(ISignetsBusinessService));
            }
		}
	}
}
