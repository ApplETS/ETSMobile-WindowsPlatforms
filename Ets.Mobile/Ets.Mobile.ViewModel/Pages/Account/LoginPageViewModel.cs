using Akavache;
using Ets.Mobile.Client;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Account;
using Ets.Mobile.ViewModel.Pages.Main;
using Logger;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using Refit;
using Security.Contracts;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Account
{
    [DataContract]
    public class LoginPageViewModel : ViewModelBase, ILoginPageViewModel
    {
        /// <summary>
        /// Constructor LoginPageViewModel
        /// </summary>
        public LoginPageViewModel(IScreen screen) : base(screen, "Login")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            UserName = string.Empty;
            Password = string.Empty;
            LogSubject = new ReplaySubject<string>(1);
            LogSubject.Subscribe(message => this.Log().Info(message));

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
                LogSubject.OnNext("Navigate to MainPageViewModel");
                HostScreen.Router.NavigateAndReset.Execute(new MainPageViewModel(HostScreen));
            });

            Login.ThrownExceptions.Subscribe(LoginThrownExceptionImpl);
        }

        private async Task<EtsUserCredentials> LoginImpl()
        {
            _isValidating = true;

            LogSubject.OnNext("Start Loging In");
            LogSubject.OnNext($"Send Request to Login for {UserName}");

            var checkCredentialsTask = ClientServices().SignetsService.Login(UserName, Password);
            // The Login sometimes takes way too long to return a value (more than 5 minutes sometimes, when other times it takes under a second)
            var fetchLogin = await Task.WhenAny(checkCredentialsTask, Task.Delay(LoginTimeoutMs));
            if (fetchLogin != checkCredentialsTask)
            {
                // Operation has timed out
                throw new SignetsException(Resources().GetStringForKey("LoginTimeoutMessage"));
            }

            LogSubject.OnNext(checkCredentialsTask.Result ? "Authentificated sucessfully" : "Invalid credentials");

            if (!checkCredentialsTask.Result)
            {
                // invalid credentials
                throw new SignetsException(Resources().GetStringForKey("LoginInvalidCredentialsMessage"));
            }

            LogSubject.OnNext("Storing your credentials securely");
            var credentials = new EtsUserCredentials(UserName, Password);
            await Cache.InsertObject(ViewModelKeys.Login, credentials).ToTask();

            LogSubject.OnNext("Set credentials in the services");
            Locator.Current.GetService<ISsoService>().SetCredentials(credentials);

            LogSubject.OnNext("Save the credentials for logging");
            Locator.Current.GetService<IUserEnabledLogger>().SetUser(Locator.Current.GetService<ISecurityProvider>().HashMd5(credentials.Username));

            LogSubject.OnNext("Load details about the logged user");
            SideNavigation.UserDetails.LoadProfile.Execute(null);

            LogSubject.OnNext("Preload courses to have the colors ready on all pages");
            var coursesTask = await ClientServices().SignetsService.Courses().ApplyCustomColors(SettingsService());
            await Cache.InsertObject(ViewModelKeys.Courses, coursesTask).ToTask();

            LogSubject.OnNext("Retrieving schedule");
            var semesters = await ClientServices().SignetsService.Semesters();
            await Cache.InsertObject(ViewModelKeys.Semesters, semesters).ToTask();
            var currentSemester = semesters.FirstOrDefault(y => y.StartDate <= DateTime.Now && y.EndDate > DateTime.Now);
            if (currentSemester != null)
            {
                var schedule = await ClientServices().SignetsService.Schedule(currentSemester.AbridgedName).ApplyCustomColors(SettingsService());

                await Cache.InsertObject(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName), schedule).ToTask();
                LogSubject.OnNext("Saved the schedule");
            }

            LogSubject.OnNext("Register Schedule Tile and LockScreen Updater");
            await Agent.ScheduleTileUpdaterBackgroundTask.Register();
            await Cache.InsertObject(ViewModelKeys.ScheduleTileUpdaterActive, true).ToTask();

            LogSubject.OnNext("Completed login flow");
            
            _isValidating = false;

            return credentials;
        }

        private void LoginThrownExceptionImpl(Exception ex)
        {
            var apiException = ex as ApiException;
            var agregateException = ex as AggregateException;
            var isNetworkExceptionRelated = apiException != null || agregateException?.InnerExceptions?.First() is ApiException;

            var messageToDisplay = isNetworkExceptionRelated ? new ErrorMessageContent(Resources().GetStringForKey("NetworkError"), Resources().GetStringForKey("NetworkTitleError"), apiException) : new ErrorMessageContent(ex.Message, ex);

            if (apiException == null)
            {
                ViewServices().Popup.ShowMessage(messageToDisplay);
            }
            else
            {
                ViewServices().Popup.ShowMessage(messageToDisplay.Message, messageToDisplay.Title);
            }

            UserError.Throw(messageToDisplay.Message, ex);
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

        public ISubject<string> LogSubject { get; set; } 

        #endregion
    }
}