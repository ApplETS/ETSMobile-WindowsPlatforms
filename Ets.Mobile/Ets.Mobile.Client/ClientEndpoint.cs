using Ets.Mobile.Client.Contracts;
using Splat;
using System;

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

        private ISignetsService _signetsService;
		/// <summary>
		/// Signets Endpoint
		/// </summary>
		public ISignetsService SignetsService => _signetsService ?? (_signetsService = _serviceLocator.GetService<ISignetsService>());

        private IMoodleService _moodleService;
        /// <summary>
		/// Signets Endpoint
		/// </summary>
		public IMoodleService MoodleService => _moodleService ?? (_moodleService = _serviceLocator.GetService<IMoodleService>());
    }
}