using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Ets.Mobile.ViewModel.Mixins;
using Messaging.Interfaces.Popup;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.ViewModel.Pages.UserDetails
{
    [DataContract]
    public class UserDetailsViewModel : PageViewModelBase, IUserDetailsViewModel
    {
        public UserDetailsViewModel(IScreen screen) : base(screen, "UserDetails")
        {
            OnViewModelCreation();
        }
        
        protected sealed override void OnViewModelCreation()
        {
            LoadProfile = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return Cache.GetAndFetchLatest(ViewModelKeys.UserProfile, () => ClientServices().SignetsService.UserDetails())
                    .Do(ud => Cache.LoadImage(ViewModelKeys.Gravatar)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Catch<IBitmap, KeyNotFoundException>(x => Observable.Empty<IBitmap>())
                        .Where(x => x != null)
                        .Subscribe(image => ud.Image = image));
            });

            LoadProfile.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                    x.HandleOfflineConnection(ViewServices().Notification);
                });

            LoadProfile.Subscribe(profile =>
            {
                Profile = profile;
            });
        }

        #region Properties

        private UserDetailsVm _profile;
        [DataMember]
        public UserDetailsVm Profile
        {
            get { return _profile; }
            set { _profile = value; this.RaisePropertyChanged(); }
        }
        //public IReactivePresenterViewModel<UserDetailsVm> UserDetailsPresenter { get; protected set; }
        public ReactiveCommand<UserDetailsVm> LoadProfile { get; protected set; }
        private readonly ReplaySubject<Exception> _profileExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}