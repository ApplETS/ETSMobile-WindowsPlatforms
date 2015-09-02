using System;
using Splat;
using StoreFramework.Composite;

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

			var types = new[]
			{
				typeof (Entities.ModuleInit),
				typeof (Module.StoreFramework.ModuleInit),
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
