using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Services
{
    public class SsoService : ISsoService
    {
        private readonly List<ISetCredentials> _services;

        public SsoService(params ISetCredentials[] services)
        {
            if (services == null || services.Length == 0)
            {
                throw new ArgumentException("There should be at least one service required for Single Sign On");
            }

            _services = services.ToList();
        }
        
        public void SetCredentials(EtsUserCredentials credentials)
        {
            foreach (var service in _services)
            {
                service.SetCredentials(credentials);
            }
        }
    }
}