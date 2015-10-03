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
using Ets.Mobile.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Logger;

namespace Ets.Mobile.ViewModel
{
	public class ApplicationShell : ReactiveObject, IScreen
	{
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public RoutingState Router { get; protected set; }
        
        public ApplicationShell()
		{
            Router = new RoutingState();

#if WINDOWS_PHONE_APP
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
            // Temporary Brute Force Akavache's Registration
            //Locator.CurrentMutable.RegisterCaching();

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

            // Navigate to Main if Authentificated
            HandleAuthentificated();
		}

        #region Verify Authentification

	    private void HandleAuthentificated()
	    {
	        BlobCache.UserAccount.GetObject<SignetsAccountVm>(ViewModelKeys.Login)
                .Catch(Observable.Return(new SignetsAccountVm()))
	            .Subscribe(signetsAccountVm =>
	            {
	                if (signetsAccountVm.IsLoginSuccessful)
	                {
                        Locator.Current.GetService<ISignetsService>().SetCredentials(signetsAccountVm);
                        Locator.Current.GetService<IUserEnabledLogger>().SetUser(signetsAccountVm.Username);
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
			Shared.ModuleInit _module = new Shared.ModuleInit();
            _module.Initialize(resolver);
		}

        private static void RegisterViewModelAndPages(IMutableDependencyResolver resolver)
		{
            // Register Views for the Router
            resolver.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            resolver.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
            resolver.Register(() => new SchedulePage(), typeof(IViewFor<ScheduleViewModel>));
            resolver.Register(() => new GradePage(), typeof(IViewFor<GradeViewModel>));
        }

		#endregion
    }
}
