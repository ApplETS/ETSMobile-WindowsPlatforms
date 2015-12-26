using Akavache;
using Ets.Mobile.Agent;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Pages.Account;
using Ets.Mobile.Pages.Grade;
using Ets.Mobile.Pages.Main;
using Ets.Mobile.Pages.Program;
using Ets.Mobile.Pages.Schedule;
using Ets.Mobile.Pages.Settings;
using Ets.Mobile.UserErrorHandlers;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Ets.Mobile.ViewModel.Pages.Account;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;
using Ets.Mobile.ViewModel.Pages.Shared;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using Logger;
using ModernHttpClient;
using ReactiveUI;
using Refit;
using Security.Algorithms;
using Splat;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using CrittercismSDK;
using Ets.Mobile.Shared;

namespace Ets.Mobile.ViewModel
{
    public interface IApplicationShell : IScreen
    {
        ISideNavigationViewModel SideNavigation { get; }
        void HandleAuthentificated();
    }

    public class ApplicationShell : ReactiveObject, IApplicationShell
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public RoutingState Router { get; protected set; }
        
	    private ISideNavigationViewModel _sideNavigation;
	    public ISideNavigationViewModel SideNavigation => _sideNavigation ?? (_sideNavigation = Locator.Current.GetService<ISideNavigationViewModel>());

	    public ApplicationShell()
	    {
            // Router for Navigation
            Router = new RoutingState();

            // Register this screen
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            Locator.CurrentMutable.RegisterLazySingleton(() => new UserDetailsViewModel(this), typeof(IUserDetailsViewModel));
            Locator.CurrentMutable.RegisterConstant(_sideNavigation = new SideNavigationViewModel(this), typeof(ISideNavigationViewModel));

#if WINDOWS_PHONE_APP
            // Back Button Handling
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += (sender, e) =>
            {
                if (SideNavigation.IsSideNavigationVisible)
                {
                    SideNavigation.CloseMenu.Execute(null);
                    e.Handled = true;
                    return;
                }
                if (Router.NavigationStack.Count > 1)
                {
                    Router.NavigateBack.Execute(null);
                    e.Handled = true;
                }
            };
#endif
            // Set up Akavache
            // 
            // Akavache is a portable data serialization library that we'll use to
            // cache data that we've downloaded
            BlobCache.ApplicationName = "EtsMobile";
            
            // Handle Application Exceptions
	        HandleApplicationExceptions();

            // Ensure we know if the phone has connectivity or not
            SetInitialConnectivityForOfflineConnections();
            
            // Set up Services
            //
            // Setting up services
            RegisterContainer(Locator.CurrentMutable);

            // Register OfflineHandler
            //
            // This handler makes easy to tell the user (only once!) that he is currently
            // offline, removing the hassle to look for his connection each time we request.
            // Added bonus: each UserError will get handled and message the user based on
            // the internet availability.
	        RegisterBackgroundTasks();

            // Set up ViewModels and Views
            //
            // Setting up the Views and assign them to their corresponding ViewModels
            RegisterViewModelAndPages(Locator.CurrentMutable);
        }
        
        private async void SetInitialConnectivityForOfflineConnections()
        {
            // When the user is launching the app, ensure that the connectivity states are reset-ed
            await HandleOfflineTask.SetConnectivityValues();
            await BlobCache.UserAccount.InsertObject("HasUserBeenNotified", false).ToTask();
        }
        
        private void HandleApplicationExceptions()
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                LoadingApplicationText = Locator.Current.GetService<ResourceLoader>().GetString("ApplicationLoadingBackgroundServices");
            });

            // Crittercism
            Crittercism.Init("55e87dc18d4d8c0a00d07811");

            // Ensure unobserved task exceptions (unawaited async methods returning Task or Task<T>) are handled
            TaskScheduler.UnobservedTaskException += (sender, e) => Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] {sender.ToString()} - observed:{e.Observed} - {e.Exception.Message}", e.Exception));
        }

        public void HandleAuthentificated()
	    {
	        BlobCache.UserAccount.GetObject<SignetsAccountVm>(ViewModelKeys.Login)
                .Catch(Observable.Return(new SignetsAccountVm()))
	            .Subscribe(signetsAccountVm =>
	            {
	                object navigateTo;
	                if (signetsAccountVm.IsLoginSuccessful)
	                {
	                    Locator.Current.GetService<ISignetsService>().SetCredentials(signetsAccountVm);
	                    Locator.Current.GetService<IUserEnabledLogger>()
	                        .SetUser(Md5Hash.GetHashString(signetsAccountVm.Username));
	                    SideNavigation.UserDetails.LoadProfile.Execute(null);
                        navigateTo = new MainViewModel(this);
	                }
	                else
	                {
                        navigateTo = new LoginViewModel(this);
                    }

	                RxApp.MainThreadScheduler.Schedule(() =>
	                {
                        LoadingApplicationText = Locator.Current.GetService<ResourceLoader>().GetString("ApplicationLoadingEnds");
                        Router.Navigate.Execute(navigateTo);
	                    LoadingApplicationText = string.Empty;
	                });
	            });
        }

        private string _loadingApplicationText;
        public string LoadingApplicationText
        {
            get { return _loadingApplicationText; }
            set { this.RaiseAndSetIfChanged(ref _loadingApplicationText, value); }
        }

        #region Registration

        private static void RegisterContainer(IMutableDependencyResolver resolver)
		{
            ModuleInit.Initialize(resolver);
		}

        private void RegisterViewModelAndPages(IMutableDependencyResolver resolver)
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                LoadingApplicationText = Locator.Current.GetService<ResourceLoader>().GetString("ApplicationLoadingViews");
            });
            // Register Views for the Router
            resolver.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            resolver.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
            resolver.Register(() => new SchedulePage(), typeof(IViewFor<ScheduleViewModel>));
            resolver.Register(() => new GradePage(), typeof(IViewFor<GradeViewModel>));
            resolver.Register(() => new ProgramPage(), typeof(IViewFor<ProgramViewModel>));
            resolver.Register(() => new SelectCourseForGradePage(), typeof(IViewFor<SelectCourseForGradeViewModel>));
            resolver.Register(() => new SettingsPage(), typeof(IViewFor<SettingsViewModel>));
        }

        private static async void RegisterBackgroundTasks()
        {
            await HandleOfflineTask.Register();
            UserError.RegisterHandler(UserErrorOfflineHandler.Handler);
            UserErrorOfflineHandler.ExceptionsHandled.Add(typeof(ApiException));
        }

        #endregion
    }
}