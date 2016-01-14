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
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Account
{
    [DataContract]
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor LoginViewModel
        /// </summary>
        public LoginViewModel(IScreen screen) : base(screen, "Login")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            UserName = string.Empty;
            Password = string.Empty;

            _isValidating = false;

            // Can Login Execute
            var userNameChanged = this.WhenAny(vm => vm.UserName, x => !string.IsNullOrEmpty(x.Value));
            var passwordChanged = this.WhenAny(vm => vm.Password, changed => !string.IsNullOrWhiteSpace(changed.Value));
            var isValidatingChanged = this.WhenAny(vm => vm._isValidating, changed => !changed.Value);
            var canLoginExecute = passwordChanged.CombineLatest(userNameChanged, isValidatingChanged, 
                (validPass, validUserName, isNotValidating) => validPass & validUserName & isNotValidating
            );

            Login = ReactiveCommand.CreateAsyncTask(canLoginExecute, async _ => await LoginImpl());

            Login.Subscribe(accountVm => {
                this.Log().Info("Navigate to MainViewModel");
                HostScreen.Router.NavigateAndReset.Execute(new MainViewModel(HostScreen));
            });

            Login.ThrownExceptions.Subscribe(LoginThrownExceptionImpl);
                }

        private async Task<EtsUserCredentials> LoginImpl()
        {
            _isValidating = true;
            this.Log().Info("Start Loging In");

            this.Log().Info($"Send Request to Login for {UserName}");
            
            var checkCredentialsTask = ClientServices().SignetsService.Login(UserName, Password);
            // The Login sometimes takes way too long to return a value (more than 5 minutes sometimes, when other times it takes under a second)
            var fetchLogin = await Task.WhenAny(checkCredentialsTask, Task.Delay(LoginTimeoutMs));
            if (fetchLogin != checkCredentialsTask)
            {
                // Operation has timed out
                throw new SignetsException(Resources().GetString("LoginTimeoutMessage"));
            }
            this.Log().Info($"Received Response for and {UserName} " + (checkCredentialsTask.Result ? "has been authentificated sucessfully" : "has invalid credentials"));

            if (!checkCredentialsTask.Result)
            {
                // invalid credentials
                throw new SignetsException(Resources().GetString("LoginInvalidCredentialsMessage"));
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

            this.Log().Info("Get the current schedule for the background service");
            var getCurrentScheduleTask = Task.Run(async () =>
            {
                var semesters = await ClientServices().SignetsService.Semesters();
                await Cache.InsertObject(ViewModelKeys.Semesters, semesters).ToTask();
                var currentSemester = semesters.FirstOrDefault(y => y.StartDate <= DateTime.Now && y.EndDate > DateTime.Now);
                if (currentSemester != null)
                {
                    var schedule = await ClientServices().SignetsService.Schedule(currentSemester.AbridgedName).ApplyCustomColors(SettingsService());
                    await Cache.InsertObject(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName), schedule).ToTask();
                }
            });

            Task.WaitAll(coursesTask, getCurrentScheduleTask);
            await Cache.InsertObject(ViewModelKeys.Courses, coursesTask.Result).ToTask();

            this.Log().Info("Register Schedule Tile and LockScreen Updater");
            await Agent.ScheduleTileUpdaterBackgroundTask.Register();
            await Cache.InsertObject(ViewModelKeys.ScheduleTileUpdaterActive, true).ToTask();

            this.Log().Info("Completed login flow");
            _isValidating = false;

            return credentials;
        }

        private void LoginThrownExceptionImpl(Exception ex)
        {
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
        }

        #region Properties

        [DataMember]
        private string _userName;
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set { this.RaiseAndSetIfChanged(ref _userName, value); }
        }

        [IgnoreDataMember]
        private string _password;
        [IgnoreDataMember]
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        private bool _isValidating;

        private ReactiveCommand<bool> _switchToLogin;
        public ReactiveCommand<bool> SwitchToLogin
        {
            get { return _switchToLogin; }
            set { this.RaiseAndSetIfChanged(ref _switchToLogin, value); }
        }

        public ReactiveCommand<EtsUserCredentials> Login { get; set; }

        private const int LoginTimeoutMs = 5000;

        #endregion
    }
}