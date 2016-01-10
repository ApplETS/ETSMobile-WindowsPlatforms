using CrittercismSDK;
using Ets.Mobile.Logger;
using Ets.Mobile.Shell;
using Ets.Mobile.TaskObservers;
using Ets.Mobile.ViewModel;
using Logger.SplatLog;
using ReactiveUI;
using Splat;
using System;
using System.Diagnostics.Tracing;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Ets.Mobile
{
    public sealed partial class App : Application
	{
	    private readonly AutoSuspendHelper _autoSuspendHelper;
        private TransitionCollection _transitions;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ResourceLoader(), typeof(ResourceLoader));
            
            InitializeComponent();

            // Crittercism
            Crittercism.Init("55e87dc18d4d8c0a00d07811");

            UnhandledException += (sender, e) => Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] {sender.ToString()} - wasHandled:{e.Handled} - {e.Message}", e.Exception));
            
            // Default Universal Behavior
            Suspending += OnSuspending;

            // Initialize Rx App
            _autoSuspendHelper = new AutoSuspendHelper(this);
            RxApp.SuspensionHost.CreateNewAppState = () => new ApplicationShell();
            RxApp.SuspensionHost.SetupDefaultSuspendResume();
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
            
            // Do RxApp OnLaunched
            _autoSuspendHelper.OnLaunched(e);

            // IAsyncCommand are handled here
            // We need a Synchronization Context to use this
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

                // Initialize Loggers
                var verboseListener = new StorageFileEventListener("ListenerVerbose");
                var informationListener = new StorageFileEventListener("ListenerInformation");
                var errorListener = new StorageFileEventListener("ListenerError");
                var criticalListener = new StorageFileEventListener("ListenerCritical");
                var warningListener = new StorageFileEventListener("ListenerWarning");

                verboseListener.EnableEvents(SplatEventSource.Log, EventLevel.Verbose);
                informationListener.EnableEvents(SplatEventSource.Log, EventLevel.Informational);
                errorListener.EnableEvents(SplatEventSource.Log, EventLevel.Error);
                criticalListener.EnableEvents(SplatEventSource.Log, EventLevel.Critical);
                warningListener.EnableEvents(SplatEventSource.Log, EventLevel.Warning);

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
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
        }

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
                rootFrame.ContentTransitions = _transitions ?? new TransitionCollection { /*new NavigationThemeTransition()*/ };
                rootFrame.Navigated -= RootFrame_FirstNavigated;
            }
        }

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