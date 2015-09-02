using System;
using Ets.Mobile.Business.Contracts;
using Splat;

namespace Ets.Mobile.Business
{
    public class BusinessEndpoint
    {
		#region Extension Point

        public IMutableDependencyResolver _serviceLocator
		{
			get;
			private set;
		}

        public BusinessEndpoint(IMutableDependencyResolver serviceLocator)
		{
            if (serviceLocator == null)
			{
                throw new ArgumentNullException("serviceLocator");	
			}

			_serviceLocator = serviceLocator;
		}

		#endregion

		/// <summary>
		/// Signets Endpoint
		/// </summary>
		public ISignetsBusinessService SignetsService => _serviceLocator.GetService<ISignetsBusinessService>();
    }
}
