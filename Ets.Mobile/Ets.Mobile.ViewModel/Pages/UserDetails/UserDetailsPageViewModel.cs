using Akavache;
using Ets.Mobile.Client.Extensions.Signets;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Pages.UserDetails
{
    [DataContract]
    public class UserDetailsPageViewModel : ViewModelBase, IUserDetailsPageViewModel
    {
        public UserDetailsPageViewModel(IScreen screen) : base(screen, "UserDetails")
        {
            OnViewModelCreation();
        }
        
        protected sealed override void OnViewModelCreation()
        {
            LoadProfile = ReactiveCommand.CreateAsyncObservable(_ => FetchProfileImpl());

            LoadProfile.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            LoadProfile.Subscribe(profile =>
            {
                Profile = profile;
            });
        }

        private IObservable<UserDetailsVm> FetchProfileImpl()
        {
            var fetchProfile = Cache.GetAndFetchLatest(ViewModelKeys.UserProfile, () => ClientServices().SignetsService.UserDetails().LoadUserImage());

            var loadProfileUserImage =
                fetchProfile
                    .Do(ud => Cache.LoadImage(ViewModelKeys.Gravatar)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Catch<IBitmap, KeyNotFoundException>(x => Observable.Empty<IBitmap>())
                    .Where(x => x != null)
                    .Subscribe(image => ud.Image = image));

            return loadProfileUserImage;
        }

        #region Properties

        private UserDetailsVm _profile;
        [DataMember]
        public UserDetailsVm Profile
        {
            get { return _profile; }
            set { _profile = value; this.RaisePropertyChanged(); }
        }
        public ReactiveCommand<UserDetailsVm> LoadProfile { get; protected set; }

        #endregion
    }
}