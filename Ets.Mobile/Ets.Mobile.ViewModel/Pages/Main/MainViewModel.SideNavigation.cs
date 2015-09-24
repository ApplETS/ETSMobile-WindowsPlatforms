using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Resources;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Pages.Account;
using ReactiveUI;
using Refit;
using Splat;
using StoreFramework.Controls.Presenter.Exceptions;
using StoreFramework.Messaging.Common;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainViewModel
    {
        private void InitializeSideNavigation()
        {
            Profile = new UserDetailsVm();

            LoadProfile = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return Observable.Defer(() =>
                {
                    Cache.GetAllKeys().Subscribe(x => string.Join(",", x));
                    return Cache.GetAndFetchLatest(ViewModelKeys.UserProfile, () => ClientServices().SignetsService.UserDetails())
                        .Do(ud => Cache.LoadImage(ViewModelKeys.Gravatar)
                            .ObserveOn(RxApp.MainThreadScheduler)
                            .Catch<IBitmap, KeyNotFoundException>(x => Observable.Empty<IBitmap>())
                            .Where(x => x != null)
                            .Subscribe(image => ud.Image = image));
                });
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
                            exceptionMessage.Content.Message = Locator.Current.GetService<ResourceLoader>().GetString("NetworkError");
                            exceptionMessage.Content.Title = Locator.Current.GetService<ResourceLoader>().GetString("NetworkTitleError");
                        }
                        exception = exceptionMessage;
                    }
                    else if (x is ReactivePresenterExceptionBase)
                    {
                        var exceptionMessage = new ErrorMessageContent(x.Message, x);
                        exception = exceptionMessage;
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

            Logout = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                await Cache.InvalidateAll().ToTask();
                HostScreen.Router.NavigateAndReset.Execute(new LoginViewModel(HostScreen));
                return Unit.Default;
            });
        }

        #region Properties

        private UserDetailsVm _profile;
        [DataMember] public UserDetailsVm Profile
        {
            get { return _profile; }
            set { this.RaiseAndSetIfChanged(ref _profile, value); }
        }

        public ReactiveCommand<UserDetailsVm> LoadProfile { get; protected set; }
        private readonly ReplaySubject<Exception> _profileExceptionSubject = new ReplaySubject<Exception>();

        public ReactiveCommand<Unit> Logout { get; set; }

        #endregion
    }
}
