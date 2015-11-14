using System.Runtime.Serialization;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.ViewModel.Bases
{
    [DataContract]
    public abstract class PageViewModelBase : ApplicationViewModelBase, IRoutableViewModel
    {
        /// <summary>
        /// <para>View ModelGroup Base</para>
        /// <para>NOTE: Use only this if your ViewModel are Pages, not Items</para>
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="urlPathSegment"></param>
        protected PageViewModelBase(IScreen screen, string urlPathSegment)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            UrlPathSegment = urlPathSegment;
        }

        [IgnoreDataMember]
        public IScreen HostScreen { get; protected set; }

        [IgnoreDataMember]
        public string UrlPathSegment { get; protected set; }
    }
}
