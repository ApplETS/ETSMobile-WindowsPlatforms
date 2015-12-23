using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations;
using Ets.Mobile.Client.Services;
using Moduler;
using Splat;

namespace Ets.Mobile.Client
{
    public class ModuleInit : IModuleInitializer
    {
        public void Initialize(IMutableDependencyResolver container)
		{
            container.RegisterLazySingleton(() => new SignetsFactory(), typeof(SignetsAbstractFactory));
            var signetServiceInstance = new SignetsService(
                container.GetService<ISignetsBusinessService>(),
                container.GetService<SignetsAbstractFactory>()
            );
            container.RegisterLazySingleton(() => signetServiceInstance, typeof(ISignetsService));
            container.RegisterLazySingleton(() => new CustomSettingsService(), typeof(ICustomSettingsService));
		}
	}
}
