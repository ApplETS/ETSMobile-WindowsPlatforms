using Windows.ApplicationModel;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.DesignTime;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations;
using Ets.Mobile.Client.Services;
using Splat;
using StoreFramework.Composite;
using StoreFramework.Logger;

namespace Ets.Mobile.Client
{
    public class ModuleInit : IModuleInitializer
    {
        public void Initialize(IMutableDependencyResolver container)
		{
            if (DesignMode.DesignModeEnabled)
            {
                container.Register(() => new DtSignetsService(container.GetService<ISignetsBusinessService>()), typeof(ISignetsService));
                container.Register(() => new DtCustomServiceSettings(), typeof(ICustomSettingsService));
            }
            else
            {
                container.Register(() => new SignetsFactory(), typeof(SignetsAbstractFactory));
                var signetServiceInstance = new SignetsService(
                    container.GetService<ISignetsBusinessService>(),
                    container.GetService<SignetsAbstractFactory>()
                );
                container.Register(() => signetServiceInstance, typeof(ISignetsService));
                container.Register(() => new CustomSettingsService(), typeof(ICustomSettingsService));
            }
		}
	}
}
