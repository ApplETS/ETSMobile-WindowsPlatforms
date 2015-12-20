using System.Runtime.Serialization;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.ViewModel.Bases
{
    [DataContract]
    public abstract class ViewModelBase : ApplicationServicesBase, IRoutableViewModel
    {
        /// <summary>
        /// Provides the basic implementation of a ViewModel
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="urlPathSegment"></param>
        protected ViewModelBase(IScreen screen, string urlPathSegment)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            UrlPathSegment = urlPathSegment;
        }
        
        protected abstract void OnViewModelCreation();

        [IgnoreDataMember]
        public IScreen HostScreen { get; protected set; }

        [IgnoreDataMember]
        public string UrlPathSegment { get; protected set; }
    }
}
