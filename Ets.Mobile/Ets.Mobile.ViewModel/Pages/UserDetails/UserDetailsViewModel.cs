using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Text;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.ViewModel;
using Refit;

namespace Ets.Mobile.ViewModel.Pages.UserDetails
{
    public class UserDetailsViewModel : PageViewModelBase
    {
        public UserDetailsViewModel(IScreen screen) : base(screen, "UserDetails")
        {
            OnViewModelCreation();
        }
        
        protected override sealed void OnViewModelCreation()
        {
            LoadInformations = ReactiveDeferedCommand.CreateAsyncObservable(() =>
                Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.UserDetails())
            );

            LoadInformations.ThrownExceptions
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
                    _userDetailsExceptionSubject.OnNext(exception);
                });

            LoadInformations.Subscribe(x =>
            {
                UserInformations = x;
            });
        }

        #region Properties

        [DataMember]
        public UserDetailsVm UserInformations { get; protected set; }
        public IReactivePresenterViewModel<UserDetailsVm> UserDetailsPresenter { get; protected set; }
        public ReactiveCommand<UserDetailsVm> LoadInformations { get; protected set; }
        private readonly ReplaySubject<Exception> _userDetailsExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}
