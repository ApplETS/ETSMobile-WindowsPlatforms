using System;
using Ets.Mobile.Client.Contracts;
using Splat;

namespace Ets.Mobile.Client
{
    public class ClientEndpoint
    {
        #region Extension Point

        private readonly IMutableDependencyResolver _serviceLocator;

        public ClientEndpoint(IMutableDependencyResolver serviceLocator)
		{
			if (serviceLocator == null)
			{
				throw new ArgumentNullException(nameof(serviceLocator));	
			}

			_serviceLocator = serviceLocator;
		}

		#endregion

		/// <summary>
		/// Signets Endpoint
		/// </summary>
		public ISignetsService SignetsService => _serviceLocator.GetService<ISignetsService>();
    }
}
