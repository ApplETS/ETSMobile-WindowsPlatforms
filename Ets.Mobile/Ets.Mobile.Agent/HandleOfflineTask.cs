using Akavache;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Networking.Connectivity;

namespace Ets.Mobile.Agent
{
    public sealed class HandleOfflineTask : IBackgroundTask
    {
        private const string TaskName = "HandleOfflineTask";
        private const string TaskEntryPoint = "Ets.Mobile.Agent.HandleOfflineTask";

        public static IAsyncAction Register()
        {
            return Task.Run(async () =>
            {
                await SetConnectivityValues();
                var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
                if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                    backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    foreach (var task in BackgroundTaskRegistration.AllTasks.Where(task => task.Value.Name == TaskName))
                    {
                        task.Value.Unregister(true);
                    }

                    var taskBuilder = new BackgroundTaskBuilder
                    {
                        Name = TaskName,
                        TaskEntryPoint = TaskEntryPoint
                    };
                    taskBuilder.SetTrigger(new SystemTrigger(SystemTriggerType.NetworkStateChange, false));
                    taskBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                    taskBuilder.Register();
                }
            }).AsAsyncAction();
        }

        public static IAsyncAction SetConnectivityValues()
        {
            return Task.Run(async () =>
            {
                var profile = NetworkInformation.GetInternetConnectionProfile();
                var wasOffline = await BlobCache.UserAccount.GetObject<bool>("IsCurrentlyOffline")
                        .Catch(Observable.Return(false)).ToTask();

                if ((!wasOffline && profile == null) ||
                    profile != null)
                {
                    await BlobCache.UserAccount.InsertObject("HasUserBeenNotified", false).ToTask();
                }

                await BlobCache.UserAccount.InsertObject("IsCurrentlyOffline", profile == null).ToTask();
            }).AsAsyncAction();
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BlobCache.ApplicationName = "EtsMobile";
            var deferral = taskInstance.GetDeferral();

            Task.WaitAll(Task.Run(async () => await SetConnectivityValues()));

            deferral.Complete();
        }
    }
}