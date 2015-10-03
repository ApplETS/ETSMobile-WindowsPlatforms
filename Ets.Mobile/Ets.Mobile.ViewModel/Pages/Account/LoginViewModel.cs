using System;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Client;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Pages.Main;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Exceptions;
using Refit;

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
                var apiException = ex as ApiException;
                var exception = apiException != null ? new ErrorMessageContent(Resources().GetString("NetworkError"), Resources().GetString("NetworkTitleError"), apiException) : new ErrorMessageContent(ex.Message, ex);
                
                if (apiException == null)
                {
                    ViewServices().Popup.ShowMessage(exception);
                }
                else
                {
                    ViewServices().Popup.ShowMessage(exception.Message, exception.Title);
                }

                UserError.Throw(exception.Message, ex);
            });
        }

        public ReactiveCommand<bool> SwitchToLogin { get; set; }

        public ReactiveCommand<SignetsAccountVm> SubmitCommand { get; set; }
    }
}
