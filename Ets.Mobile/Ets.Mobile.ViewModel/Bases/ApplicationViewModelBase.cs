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
    public abstract class ApplicationViewModelBase : ReactiveObject
    {
        protected ApplicationViewModelBase()
        {
            _serviceLocator = Locator.CurrentMutable;
        }
        
        [IgnoreDataMember]
        private readonly IMutableDependencyResolver _serviceLocator;

        protected abstract void OnViewModelCreation();

        [IgnoreDataMember] private BusinessEndpoint _be;
        public BusinessEndpoint BusinessServices()
        {
            return _be ?? (_be = new BusinessEndpoint(_serviceLocator));
        }

        [IgnoreDataMember]
        private ClientEndpoint _ce;
        public ClientEndpoint ClientServices()
        {
            return _ce ?? (_ce = new ClientEndpoint(_serviceLocator));
        }

        [IgnoreDataMember]
        private ResourceLoader _rl;
        public ResourceLoader Resources()
        {
            return _rl ?? (_rl = _serviceLocator.GetService<ResourceLoader>());
        }

        [IgnoreDataMember]
        private ICustomSettingsService _ss;
        public ICustomSettingsService SettingsService()
        {
            return _ss ?? (_ss = _serviceLocator.GetService<ICustomSettingsService>());
        }

        private IBlobCache _bc;
        public IBlobCache Cache => _bc ?? (_bc = BlobCache.UserAccount);

        private IViewService _vs;
        public IViewService ViewServices()
        {
            return _vs ?? (_vs = _serviceLocator.GetService<IViewService>());
        }

        private ISideNavigationViewModel _sideNavigation;
        public ISideNavigationViewModel SideNavigation => _sideNavigation ?? (_sideNavigation = _serviceLocator.GetService<ISideNavigationViewModel>());

        /// <summary>
        /// Gets a value that indicates whether the process is running in design mode.
        /// </summary>
        protected bool IsInDesignMode => DesignMode.DesignModeEnabled;
    }
}
