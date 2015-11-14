using ReactiveUI;
using Splat;
using ModernHttpClient;
using System.Net.Http;
using System.Reactive.Linq;
using Akavache;
using System;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Pages.Account;
using Ets.Mobile.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Account;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Pages.Grade;
using Ets.Mobile.Pages.Program;
using Ets.Mobile.Pages.Schedule;
using Ets.Mobile.Pages.Settings;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;
using Ets.Mobile.ViewModel.Pages.Shared;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using Logger;

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
            Locator.CurrentMutable.Register(() => new UserDetailsViewModel(this), typeof(IUserDetailsViewModel));
            Locator.CurrentMutable.RegisterConstant(_sideNavigation = new SideNavigationViewModel(this), typeof(ISideNavigationViewModel));

#if WINDOWS_PHONE_APP
            // Back Button Handling
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += (sender, e) =>
            {
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

            // Set up Fusillade
            //
            // Fusillade is a super cool library that will make it so that whenever
            // we issue web requests, we'll only issue 4 concurrently, and if we
            // end up issuing multiple requests to the same resource, it will
            // de-dupe them. We're saying here, that we want our *backing*
            // HttpMessageHandler to be ModernHttpClient.
            Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));

            // Set up Services
            //
            // Setting up services
            RegisterContainer(Locator.CurrentMutable);
            
            // Set up ViewModels and Views
            //
            // Setting up the Views and assign them to their corresponding ViewModels
            RegisterViewModelAndPages(Locator.CurrentMutable);
        }

        #region Verify Authentification

	    public void HandleAuthentificated()
	    {
	        BlobCache.UserAccount.GetObject<SignetsAccountVm>(ViewModelKeys.Login)
                .ObserveOn(RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Catch(Observable.Return(new SignetsAccountVm()))
	            .Subscribe(signetsAccountVm =>
	            {
	                if (signetsAccountVm.IsLoginSuccessful)
	                {
                        Locator.Current.GetService<ISignetsService>().SetCredentials(signetsAccountVm);
                        Locator.Current.GetService<IUserEnabledLogger>().SetUser(signetsAccountVm.Username);
                        SideNavigation.UserDetails.LoadProfile.Execute(null);
                        Router.Navigate.Execute(new MainViewModel(this));
	                }
                    else
	                {
                        Router.Navigate.Execute(new LoginViewModel(this));
                    }
                });
        }

        #endregion

        #region Registration

        static void RegisterContainer(IMutableDependencyResolver resolver)
		{
			var module = new Shared.ModuleInit();
            module.Initialize(resolver);
		}

        private static void RegisterViewModelAndPages(IMutableDependencyResolver resolver)
		{
            // Register Views for the Router
            resolver.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            resolver.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
            resolver.Register(() => new SchedulePage(), typeof(IViewFor<ScheduleViewModel>));
            resolver.Register(() => new GradePage(), typeof(IViewFor<GradeViewModel>));
            resolver.Register(() => new ProgramPage(), typeof(IViewFor<ProgramViewModel>));
            resolver.Register(() => new SelectCourseForGradePage(), typeof(IViewFor<SelectCourseForGradeViewModel>));
            resolver.Register(() => new SettingsPage(), typeof(IViewFor<SettingsViewModel>));
        }

        #endregion
    }
}
