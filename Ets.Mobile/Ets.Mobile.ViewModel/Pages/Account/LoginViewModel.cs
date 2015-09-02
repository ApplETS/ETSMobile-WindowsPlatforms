using System;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Resources;
using Akavache;
using Ets.Mobile.Client;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Pages.Main;
using ReactiveUI;
using Refit;
using Splat;
using StoreFramework.Controls.Presenter.Exceptions;
using StoreFramework.Messaging.Common;
using StoreFramework.Messaging.Popup;

namespace Ets.Mobile.ViewModel.Pages.Account
{
    [DataContract]
    public class LoginViewModel : PageViewModelBase
    {
        #region VM Properties
        
        [DataMember] private string _userName;
        [DataMember] public string UserName {
            get { return _userName; }
            set { this.RaiseAndSetIfChanged(ref _userName, value); }
        }

        [IgnoreDataMember] private string _password;
        [IgnoreDataMember] public string Password {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }
        
        #endregion

        private bool _isValidating;

        /// <summary>
        /// Constructor LoginViewModel
        /// </summary>
        public LoginViewModel(IScreen screen) : base(screen, "Login")
        {
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            UserName = Password = string.Empty;
            _isValidating = false;

            SetupSubmitCommand();
        }

        private void SetupSubmitCommand()
        {
            // Submit Command
            var canLogin = this.WhenAny(x => x.Password, x => !string.IsNullOrWhiteSpace(x.Value)).CombineLatest(this.WhenAny(x => x.UserName, x => !string.IsNullOrEmpty(x.Value)), this.WhenAny(x => x._isValidating, x => !x.Value), (x, y, z) => x & y & z);
            
            SubmitCommand = ReactiveCommand.CreateAsyncTask(canLogin, async _ =>
            {
                _isValidating = true;
                var signetsAccountVm = await Cache.GetAndFetchLatest(ViewModelKeys.Login,
                    async () => {
                        var isLoginSuccessful = await ClientServices().SignetsService.Login(UserName, Password);

                        return new SignetsAccountVm(UserName, Password, isLoginSuccessful);
                    }
                );

                if (!signetsAccountVm.IsLoginSuccessful)
                {
                    throw new SignetsException("Nom d'usager ou mot de passe invalide.");
                }

                _isValidating = false;

                return signetsAccountVm;
            });

            SubmitCommand.Subscribe(accountVm => {
                ClientServices().SignetsService.SetCredentials(accountVm);
                HostScreen.Router.Navigate.Execute(new MainViewModel(HostScreen));
            });

            SubmitCommand.ThrownExceptions.Subscribe(ex => {
                Exception exception;
                var apiException = ex as ApiException;
                if (apiException != null)
                {
                    var exceptionMessage = new ErrorMessageContent(ex.Message, apiException);
                    if (apiException.ReasonPhrase == "Not Found")
                    {
                        exceptionMessage.Content.Message = Locator.Current.GetService<ResourceLoader>().GetString("NetworkError");
                        exceptionMessage.Content.Title = Locator.Current.GetService<ResourceLoader>().GetString("NetworkTitleError");
                    }
                    exception = exceptionMessage;
                }
                else if (ex is ReactivePresenterExceptionBase)
                {
                    var exceptionMessage = new ErrorMessageContent(ex.Message, ex);
                    exception = exceptionMessage;
                }
                else
                {
                    exception = ex;
                }

                var errorAsPopup = exception as ErrorMessageContent;
                
                if (errorAsPopup != null)
                {
                    Locator.Current.GetService<IPopupManager>().ShowMessage(errorAsPopup.Content);
                    UserError.Throw(errorAsPopup.Content.Message, ex);
                }
                else
                {
                    if(!(exception is SignetsException))
                    {
                        Locator.Current.GetService<IPopupManager>().ShowMessage(exception.Message);
                    }
                    
                    UserError.Throw(ex.Message, ex);
                }
            });
        }

        public ReactiveCommand<bool> SwitchToLogin { get; set; }

        public ReactiveCommand<SignetsAccountVm> SubmitCommand { get; set; }
    }
}
