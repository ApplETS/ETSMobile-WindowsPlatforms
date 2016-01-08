using Ets.Mobile.Business.Contracts;
using Splat;
using System;

namespace Ets.Mobile.Business
{
    public class BusinessEndpoint
    {
		#region Extension Point

        public IMutableDependencyResolver ServiceLocator { get; }

        public BusinessEndpoint(IMutableDependencyResolver serviceLocator)
		{
            if (serviceLocator == null)
			{
                throw new ArgumentNullException(nameof(serviceLocator));	
			}

			ServiceLocator = serviceLocator;
		}

		#endregion

		/// <summary>
		/// Signets Endpoint
		/// </summary>
		public ISignetsBusinessService SignetsService => ServiceLocator.GetService<ISignetsBusinessService>();
    }
}