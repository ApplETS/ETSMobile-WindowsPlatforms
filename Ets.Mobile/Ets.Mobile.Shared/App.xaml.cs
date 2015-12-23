using Akavache;
using Ets.Mobile.Agent;
using Ets.Mobile.Shell;
using Ets.Mobile.ViewModel;
using ReactiveUI;
using Splat;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CrittercismSDK;

#if WINDOWS_PHONE_APP
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
#endif

namespace Ets.Mobile
{
	public sealed partial class App : Application
	{
        readonly AutoSuspendHelper _autoSuspendHelper;
#if WINDOWS_PHONE_APP
        private TransitionCollection _transitions;
#endif
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ResourceLoader(), typeof(ResourceLoader));
            
            InitializeComponent();

            // Akavache Init
            BlobCache.ApplicationName = "EtsMobile";

            // Crittercism
            Crittercism.Init("55e87dc18d4d8c0a00d07811");

            // ensure unobserved task exceptions (unawaited async methods returning Task or Task<T>) are handled
            TaskScheduler.UnobservedTaskException += (sender, e) => Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] {sender.ToString()} - observed:{e.Observed} - {e.Exception.Message}", e.Exception));
            
            UnhandledException += (sender, e) => Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] {sender.ToString()} - wasHandled:{e.Handled} - {e.Message}", e.Exception));
            
            // Default Universal Behavior
            Suspending += OnSuspending;

            // Initialize Rx App
            _autoSuspendHelper = new AutoSuspendHelper(this);
            RxApp.SuspensionHost.CreateNewAppState = () => new ApplicationShell();
            RxApp.SuspensionHost.SetupDefaultSuspendResume();
        }
        public class AsyncSynchronizationContext : SynchronizationContext
        {
            public static AsyncSynchronizationContext Register()
            {
                var syncContext = Current;
                if (syncContext == null)
                    throw new InvalidOperationException("Ensure a synchronization context exists before calling this method.");

                var customSynchronizationContext = syncContext as AsyncSynchronizationContext;

                if (customSynchronizationContext == null)
                {
                    customSynchronizationContext = new AsyncSynchronizationContext(syncContext);
                    SetSynchronizationContext(customSynchronizationContext);
                }

                return customSynchronizationContext;
            }

            private readonly SynchronizationContext _syncContext;

            public AsyncSynchronizationContext(SynchronizationContext syncContext)
            {
                _syncContext = syncContext;
            }

            public override SynchronizationContext CreateCopy()
            {
                return new AsyncSynchronizationContext(_syncContext.CreateCopy());
            }

            public override void OperationCompleted()
            {
                _syncContext.OperationCompleted();
            }

            public override void OperationStarted()
            {
                _syncContext.OperationStarted();
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                _syncContext.Post(WrapCallback(d), state);
            }

            public override void Send(SendOrPostCallback d, object state)
            {
                _syncContext.Send(d, state);
            }

            private static SendOrPostCallback WrapCallback(SendOrPostCallback sendOrPostCallback)
            {
                return state =>
                {
                    Exception exception = null;

                    try
                    {
                        sendOrPostCallback(state);
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }

                    if (exception != null)
                        Crittercism.LogUnhandledException(new Exception($"[WrapCallback][{DateTime.Now}] {exception.Message} -> {exception?.InnerException?.Message}", exception.InnerException));
                };
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do Base Launched
            base.OnLaunched(e);
            
            // Navigate to the Startup ViewModel When Available
            RxApp.SuspensionHost.ObserveAppState<ApplicationShell>()
                        .Subscribe(screen => screen.HandleAuthentificated());

            // When the user is launching the app, ensure that the connectivity states are reset-ed
            Task.WaitAll(Task.Run(async () =>
            {
                await HandleOfflineTask.SetConnectivityValues();
                BlobCache.UserAccount.InsertObject("HasUserBeenNotified", false);
            }));

            // Do RxApp OnLaunched
            _autoSuspendHelper.OnLaunched(e);

            // IAsyncCommand are handled here
            // We need a Synchronization Context to use this
            // Don't move it.
            AsyncSynchronizationContext.Register();

            // Create New Frame
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame
                {
                    CacheSize = 1, // Size of 1 is okay for now.
                };

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    _transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        _transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += RootFrame_FirstNavigated;
#endif
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof (ShellPage), e.Arguments))
                {
                    Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] Failed to create initial page"));
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

//#if WINDOWS_PHONE_APP
//            // Allows the windows to always use the full screen
//            // This solves the problem: when hiding the commandbar, the windows would not resize. Now it does.
//            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
//#endif
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            if (rootFrame != null)
            {
                rootFrame.ContentTransitions = _transitions ?? new TransitionCollection { new NavigationThemeTransition() };
                rootFrame.Navigated -= RootFrame_FirstNavigated;
            }
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }
    }
}