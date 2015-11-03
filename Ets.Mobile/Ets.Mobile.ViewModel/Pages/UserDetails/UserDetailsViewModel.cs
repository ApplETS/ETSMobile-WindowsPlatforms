using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.ViewModel;
using Refit;
using Splat;

namespace Ets.Mobile.ViewModel.Pages.UserDetails
{
    public class UserDetailsViewModel : PageViewModelBase, IUserDetailsViewModel
    {
        public UserDetailsViewModel(IScreen screen) : base(screen, "UserDetails")
        {
            OnViewModelCreation();
        }
        
        protected override sealed void OnViewModelCreation()
        {
            LoadProfile = ReactiveDeferedCommand.CreateAsyncObservable(() =>
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
                    Exception exception;
                    var apiException = x as ApiException;
                    if (apiException != null)
                    {
                        var exceptionMessage = new ErrorMessageContent(x.Message, apiException);
                        if (apiException.ReasonPhrase == "Not Found")
                        {
                            exceptionMessage.Message = Resources().GetString("NetworkError");
                            exceptionMessage.Title = Resources().GetString("NetworkTitleError");
                        }
                        exception = exceptionMessage.Exception;
                    }
                    else
                    {
                        exception = x;
                    }
                    _profileExceptionSubject.OnNext(exception);
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
            set { this.RaiseAndSetIfChanged(ref _profile, value); }
        }
        //public IReactivePresenterViewModel<UserDetailsVm> UserDetailsPresenter { get; protected set; }
        public ReactiveCommand<UserDetailsVm> LoadProfile { get; protected set; }
        private readonly ReplaySubject<Exception> _profileExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}
