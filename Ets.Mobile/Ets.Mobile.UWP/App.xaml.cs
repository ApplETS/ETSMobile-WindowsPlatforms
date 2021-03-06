﻿using Akavache;
using CrittercismSDK;
using Ets.Mobile.Logger;
using Ets.Mobile.Shell;
using Ets.Mobile.TaskObservers;
using Ets.Mobile.ViewModel;
using Localization.Interface.Contracts;
using Localization.Services;
using Logger.SplatLog;
using ReactiveUI;
using Splat;
using System;
using System.Diagnostics.Tracing;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ets.Mobile
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private readonly AutoSuspendHelper _autoSuspendHelper;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            var resourceLoader = new ResourceLoader();
            Locator.CurrentMutable.RegisterLazySingleton(() => resourceLoader, typeof(ResourceLoader));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ResourceLoaderContainer(resourceLoader), typeof(IResourceContainer));
            Locator.CurrentMutable.RegisterLazySingleton(() => BlobCache.UserAccount, typeof(IBlobCache));

            // Crittercism
            Crittercism.Init("55e87dc18d4d8c0a00d07811");

            InitializeComponent();

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
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do Base Launched
            base.OnLaunched(e);
            
            // Navigate to the Startup ViewModel When Available
            // Only on Windows, on Windows Phone we use the Extended Splash Screen
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.PhoneContract"))
            {
                RxApp.SuspensionHost.ObserveAppState<ApplicationShell>()
                    .ObserveOn(RxApp.TaskpoolScheduler)
                    .SubscribeOn(RxApp.TaskpoolScheduler)
                    .Subscribe(screen => screen.HandleAuthentificated());
            }

            // Do RxApp OnLaunched
            _autoSuspendHelper.OnLaunched(e);

            // IAsyncCommand are handled here
            // We need a Synchronization Context to use this
            AsyncSynchronizationContext.Register();

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

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
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof (ShellPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] Failed to create initial page"));
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
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
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}