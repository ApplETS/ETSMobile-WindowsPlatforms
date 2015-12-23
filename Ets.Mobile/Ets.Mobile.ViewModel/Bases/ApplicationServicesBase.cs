using System.Runtime.Serialization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Akavache;
using Ets.Mobile.Business;
using Ets.Mobile.Client;
using Ets.Mobile.Client.Contracts;
using Messaging.Interfaces.ViewService;
using ReactiveUI;
using Splat;
using Ets.Mobile.ViewModel.Contracts.Shared;

namespace Ets.Mobile.ViewModel.Bases
{
    [DataContract]
    public class ApplicationServicesBase : ReactiveObject
    {
        protected ApplicationServicesBase()
        {
            _serviceLocator = Locator.CurrentMutable;
        }
        
        [IgnoreDataMember]
        private readonly IMutableDependencyResolver _serviceLocator;
        
        [IgnoreDataMember] private BusinessEndpoint _be;
        public BusinessEndpoint BusinessServices()
        {
            return _be ?? (_be = new BusinessEndpoint(_serviceLocator));
        }

        [IgnoreDataMember]
        private ClientEndpoint _ce;
        protected ClientEndpoint ClientServices()
        {
            return _ce ?? (_ce = new ClientEndpoint(_serviceLocator));
        }

        [IgnoreDataMember]
        private ResourceLoader _rl;
        protected ResourceLoader Resources()
        {
            return _rl ?? (_rl = _serviceLocator.GetService<ResourceLoader>());
        }

        [IgnoreDataMember]
        private ICustomSettingsService _ss;

        protected ICustomSettingsService SettingsService()
        {
            return _ss ?? (_ss = _serviceLocator.GetService<ICustomSettingsService>());
        }

        private IBlobCache _bc;
        protected IBlobCache Cache => _bc ?? (_bc = BlobCache.UserAccount);

        private IViewService _vs;
        protected IViewService ViewServices()
        {
            return _vs ?? (_vs = _serviceLocator.GetService<IViewService>());
        }

        private ISideNavigationViewModel _sideNavigation;

        // ReSharper disable once MemberCanBeProtected.Global
        // The side navigation needs to be accessible in the views
        public ISideNavigationViewModel SideNavigation
        {
            get { return _sideNavigation ?? (_sideNavigation = _serviceLocator.GetService<ISideNavigationViewModel>()); }
        }

        /// <summary>
        /// Gets a value that indicates whether the process is running in design mode.
        /// </summary>
        protected bool IsInDesignMode => DesignMode.DesignModeEnabled;
    }
}
