using Akavache;
using CrittercismSDK;
using Ets.Mobile.Agent;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Pages.Account;
using Ets.Mobile.Pages.Grade;
using Ets.Mobile.Pages.Main;
using Ets.Mobile.Pages.Moodle;
using Ets.Mobile.Pages.Moodle.Courses;
using Ets.Mobile.Pages.Program;
using Ets.Mobile.Pages.Schedule;
using Ets.Mobile.Pages.Settings;
using Ets.Mobile.Shared;
using Ets.Mobile.UserErrorHandlers;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Ets.Mobile.ViewModel.Pages.Account;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Moodle;
using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;
using Ets.Mobile.ViewModel.Pages.Shared;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using Logger;
using ReactiveUI;
using Refit;
using Security.Contracts;
using Splat;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
#if WINDOWS_PHONE_APP
using Ets.Mobile.ViewModel.WinPhone.Pages.ExtendedSplashScreen;
using Ets.Mobile.Pages.ExtendedSplashScreen;
#elif WINDOWS_UWP
using Ets.Mobile.ViewModel.UWP.Pages.ExtendedSplashScreen;
using Ets.Mobile.Pages.ExtendedSplashScreen;
#endif

namespace Ets.Mobile.ViewModel
{
    public interface IApplicationShell : IScreen
    {
        ISideNavigationViewModel SideNavigation { get; }
        void HandleAuthentificated();
        void LoadApplicationServices();
    }

    public class ApplicationShell : ReactiveObject, IApplicationShell
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public RoutingState Router { get; protected set; }

        private readonly IMutableDependencyResolver _resolver;
        
	    private ISideNavigationViewModel _sideNavigation;
	    public ISideNavigationViewModel SideNavigation => _sideNavigation ?? (_sideNavigation = Locator.Current.GetService<ISideNavigationViewModel>());

	    public ApplicationShell()
	    {
            // Router for Navigation
            Router = new RoutingState();

	        _resolver = Locator.CurrentMutable;

            // Register this screen
            _resolver.RegisterConstant(this, typeof(IScreen));
            _resolver.RegisterLazySingleton(() => new UserDetailsViewModel(this), typeof(IUserDetailsViewModel));
            _resolver.RegisterConstant(_sideNavigation = new SideNavigationViewModel(this), typeof(ISideNavigationViewModel));

#if WINDOWS_PHONE_APP || WINDOWS_UWP
            // Back Button Handling
#if WINDOWS_UWP
            if(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
#endif
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

                // Register Extended SplashScreen for the Router
                _resolver.Register(() => new ExtendedSplashScreenPage(), typeof(IViewFor<ExtendedSplashScreenViewModel>));
                RxApp.MainThreadScheduler.Schedule(() => Router.NavigateAndReset.Execute(new ExtendedSplashScreenViewModel(this)));
#if WINDOWS_UWP
            }
#endif
#endif

#if WINDOWS_APP || WINDOWS_UWP
#if WINDOWS_UWP
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.PhoneContract"))
	        {
#endif
                LoadApplicationServices();
#if WINDOWS_UWP
            }
#endif
#endif
        }

        public void LoadApplicationServices()
        {
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
            RegisterContainer(_resolver);

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
            RegisterViewModelAndPages(_resolver);
        }
        
        private async void SetInitialConnectivityForOfflineConnections()
        {
            // When the user is launching the app, ensure that the connectivity states are reset-ed
            await HandleOfflineTask.SetConnectivityValues();
            await BlobCache.UserAccount.InsertObject("HasUserBeenNotified", false).ToTask();
        }
        
        private void HandleApplicationExceptions()
        {
            // Ensure unobserved task exceptions (unawaited async methods returning Task or Task<T>) are handled
            TaskScheduler.UnobservedTaskException += (sender, e) => Crittercism.LogUnhandledException(new Exception($"[UnhandledException][{DateTime.Now}] {sender.ToString()} - observed:{e.Observed} - {e.Exception.Message}, StackTrace:{e.Exception.StackTrace}", e.Exception));
        }

        public void HandleAuthentificated()
	    {
	        BlobCache.UserAccount.GetObject<EtsUserCredentials>(ViewModelKeys.Login)
                .Catch(Observable.Return(new EtsUserCredentials()))
	            .Subscribe(signetsAccountVm =>
	            {
	                object navigateTo;
	                if (signetsAccountVm.IsLoginSuccessful)
	                {
                        _resolver.GetService<ISsoService>().SetCredentials(signetsAccountVm);
                        _resolver.GetService<IUserEnabledLogger>().SetUser(Locator.Current.GetService<ISecurityProvider>().HashMd5(signetsAccountVm.Username));
	                    SideNavigation.UserDetails.LoadProfile.Execute(null);
                        navigateTo = new MainPageViewModel(this);
	                }
	                else
	                {
                        navigateTo = new LoginPageViewModel(this);
                    }

	                RxApp.MainThreadScheduler.Schedule(() =>
	                {
                        Router.NavigateAndReset.Execute(navigateTo);
	                });
	            });
        }

        #region Registration

        private static void RegisterContainer(IMutableDependencyResolver resolver)
		{
            ModuleInit.Initialize(resolver);
		}

        private void RegisterViewModelAndPages(IMutableDependencyResolver resolver)
        {
            // Register Views for the Router
            resolver.Register(() => new LoginPage(), typeof(IViewFor<LoginPageViewModel>));
            resolver.Register(() => new MainPage(), typeof(IViewFor<MainPageViewModel>));
            resolver.Register(() => new SchedulePage(), typeof(IViewFor<ScheduleViewModel>));
            resolver.Register(() => new GradePage(), typeof(IViewFor<GradePageViewModel>));
            resolver.Register(() => new ProgramPage(), typeof(IViewFor<ProgramViewModel>));
            resolver.Register(() => new SelectCourseForGradePage(), typeof(IViewFor<SelectCourseForGradePageViewModel>));
            resolver.Register(() => new SettingsPage(), typeof(IViewFor<SettingsViewModel>));
            resolver.Register(() => new MoodleMainPage(), typeof(IViewFor<MoodleMainPageViewModel>));
            resolver.Register(() => new MoodleCourseContentPage(), typeof(IViewFor<MoodleCourseContentPageViewModel>));
            resolver.Register(() => new MoodleCourseModulePage(), typeof(IViewFor<MoodleCourseModulePageViewModel>));
            resolver.Register(() => new MoodleCourseModuleContentPage(), typeof(IViewFor<MoodleCourseModuleContentPageViewModel>));
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