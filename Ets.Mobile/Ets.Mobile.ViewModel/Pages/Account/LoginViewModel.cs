using Akavache;
using Ets.Mobile.Client;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Pages.Main;
using Logger;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using Refit;
using Security.Algorithms;
using Splat;
using System;
using System.IO;
using System.IO.Compression;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.Storage.Streams;
using Ets.Mobile.ViewModel.Helpers;

namespace Ets.Mobile.ViewModel.Pages.Account
{
    [DataContract]
    public class LoginViewModel : ViewModelBase
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

        protected sealed override void OnViewModelCreation()
        {
            UserName = Password = string.Empty;

            _isValidating = false;

            SetupSubmitCommand();
        }

        private void SetupSubmitCommand()
        {
            // Submit Command
            var canLogin = this.WhenAny(x => x.Password, x => !string.IsNullOrWhiteSpace(x.Value)).CombineLatest(this.WhenAny(x => x.UserName, x => !string.IsNullOrEmpty(x.Value)), this.WhenAny(x => x._isValidating, x => !x.Value), (x, y, z) => x & y & z);
            
            SubmitCommand = ReactiveCommand.CreateAsyncTask(canLogin, async _ => await LoginImpl());

            SubmitCommand.Subscribe(accountVm => {
                this.Log().Info("Navigate to MainViewModel");
                HostScreen.Router.NavigateAndReset.Execute(new MainViewModel(HostScreen));
            });

            SendLogsWhenPressedFiveTimesCommand = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                if (!_hasSentLogFiles)
                {
                    if (_countBeforeSendingLogs >= 6)
                    {
                        var isExecuting = await SubmitCommand.IsExecuting.FirstAsync().ToTask();
                        await LogHelper.ZipApplicationLogsAndSendEmail($"Login Submit Command Status (isExecuting): {isExecuting}");
                        _hasSentLogFiles = true;
                    }
                    _countBeforeSendingLogs++;
                }
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

        private async Task<EtsUserCredentials> LoginImpl()
        {
            _isValidating = true;
            this.Log().Info("Start Loging In");

            this.Log().Info($"Send Request to Login for {UserName}");
            var isLoginSuccessful = await ClientServices().SignetsService.Login(UserName, Password);
            this.Log().Info($"Received Response for and {UserName} " + (isLoginSuccessful ? "has been authentificated sucessfully" : "has invalid credentials"));
            
            if (!isLoginSuccessful)
            {
                throw new SignetsException("Nom d'usager ou mot de passe invalide.");
            }

            var credentials = new EtsUserCredentials(UserName, Password);
            await Cache.InsertObject(ViewModelKeys.Login, credentials).ToTask();

            this.Log().Info("Set the credentials of the User in the Sso Service");
            Locator.Current.GetService<ISsoService>().SetCredentials(credentials);

            this.Log().Info("Set the credentials of the User in the logger");
            Locator.Current.GetService<IUserEnabledLogger>().SetUser(Md5Hash.GetHashString(credentials.Username));

            this.Log().Info("Load details about the logged user");
            SideNavigation.UserDetails.LoadProfile.Execute(null);

            this.Log().Info("Preload courses to have the colors ready on all pages");
            var coursesTask = Task.Run(async () => await ClientServices().SignetsService.Courses().ApplyCustomColors(SettingsService()));
            Task.WaitAll(coursesTask);
            await Cache.InsertObject(ViewModelKeys.Courses, coursesTask.Result).ToTask();

            this.Log().Info("Completed login flow");

            _isValidating = false;

            return credentials;
        }

        #region Send Log Files

        private bool _hasSentLogFiles;
        private int _countBeforeSendingLogs;

        #endregion

        #region Properties

        public ReactiveCommand<bool> SwitchToLogin { get; set; }

        public ReactiveCommand<EtsUserCredentials> SubmitCommand { get; set; }

        public ReactiveCommand<Unit> SendLogsWhenPressedFiveTimesCommand { get; set; }

        #endregion
    }
}