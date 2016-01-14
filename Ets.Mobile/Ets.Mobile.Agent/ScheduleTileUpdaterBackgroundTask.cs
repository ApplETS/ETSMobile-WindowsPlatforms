using Ets.Mobile.Client.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Notifications;

namespace Ets.Mobile.Agent
{
    public sealed class ScheduleTileUpdaterBackgroundTask : IBackgroundTask
    {
        public static IAsyncAction Register()
        {
            return Task.Run(async () =>
            {
                var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
                if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                    backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    // Ensure we don't register multiple times, but only once.
                    await Unregister();

                    var taskBuilder = new BackgroundTaskBuilder
                    {
                        Name = TaskName,
                        TaskEntryPoint = TaskEntryPoint
                    };
                    taskBuilder.SetTrigger(new TimeTrigger(30, false));
                    taskBuilder.Register();
                    await RunTaskAsync();
                }
            }).AsAsyncAction();
        }

        public static IAsyncAction Unregister()
        {
            return Task.Run(() =>
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks.Where(task => task.Value.Name == TaskName))
                {
                    task.Value.Unregister(true);
                }
            }).AsAsyncAction();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferal = taskInstance.GetDeferral();

            await RunTaskAsync();

            deferal.Complete();
        }

        private static async Task RunTaskAsync()
        {
            var scheduleItem =
                await LiveTileAndLockScreenService.GetCurrentOrIncomingCourse();

            if (scheduleItem != null)
            {
                UpdateTile(scheduleItem.ActivityName, scheduleItem.Location, scheduleItem.Name, scheduleItem.Group, scheduleItem.StartDate, scheduleItem.EndDate);
            }
        }

        private static void UpdateTile(string courseName, string location, string activityName, string group, DateTimeOffset startTime, DateTimeOffset endTime)
        {
#if WINDOWS_APP || WINDOWS_UWP
            // Wide
            //
            foreach (var item in ContentWide.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                {

                    switch (namedItem.NodeValue.ToString())
                    {
                        case "3":
                            item.InnerText = courseName;
                            break;
                        case "4":
                            item.InnerText = $"{location} - {activityName} (Gr: {group})";
                            break;
                        case "5":
                            item.InnerText = startTime.ToString("HH:mm tt") + " - " + endTime.ToString("HH:mm tt");
                            break;
                    }
                }
            }
#elif WINDOWS_PHONE_APP
            // Wide
            //
            foreach (var item in ContentForLockScreenWide.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                {

                    switch (namedItem.NodeValue.ToString())
                    {
                        case "1":
                            item.InnerText = courseName;
                            break;
                        case "2":
                            item.InnerText = $"{location} - {activityName} (Gr: {group})";
                            break;
                        case "3":
                            item.InnerText = startTime.ToString("HH:mm tt") + " - " + endTime.ToString("HH:mm tt");
                            break;
                    }
                }
            }
            foreach (var item in ContentForLockScreenWide.SelectNodes("//image"))
            {
                var namedItem = item.Attributes.GetNamedItem("src");
                if (namedItem != null)
                    namedItem.InnerText = "/Assets/TileImage/Badge.scale-240.png";
            }
#endif
            // Square
            //
            foreach (var item in ContentSquare150.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                    switch (namedItem.NodeValue.ToString())
                    {
                        case "1":
                            item.InnerText = courseName;
                            break;
                        case "2":
                            item.InnerText = $"{location}"
                                + "\n" + startTime.ToString("HH:mm tt") + " - " + endTime.ToString("HH:mm tt");
                            break;
                    }
            }
            foreach (var item in ContentSquare310.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                    switch (namedItem.NodeValue.ToString())
                    {
                        case "1":
                            item.InnerText = courseName;
                            break;
                        case "2":
                            item.InnerText = $"{location}"
                                + "\n" + startTime.ToString("HH:mm tt") + " - " + endTime.ToString("HH:mm tt");
                            break;
                    }
            }

            // Tile Update Manager
            //
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForSquare150x150(true);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForSquare310x310(true);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForWide310x150(true);
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
#if WINDOWS_APP || WINDOWS_UWP
            TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(ContentWide));
#elif WINDOWS_PHONE_APP
            TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(ContentForLockScreenWide));
#endif
            TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(ContentSquare150));
            TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(ContentSquare310));
        }

        #region Properties

        private const string TaskName = "ScheduTileUpdater";
        private const string TaskEntryPoint = "Ets.Mobile.Agent.ScheduleTileUpdaterBackgroundTask";
        private const TileTemplateType TileContentSquare150 = TileTemplateType.TileSquare150x150Text02;
        private const TileTemplateType TileContentSquare310 = TileTemplateType.TileSquare310x310Text02;
#if WINDOWS_PHONE_APP
        private const TileTemplateType TileForLockScreenWide = TileTemplateType.TileWide310x150IconWithBadgeAndText;
        private static readonly XmlDocument ContentForLockScreenWide = TileUpdateManager.GetTemplateContent(TileForLockScreenWide);
#else
        private const TileTemplateType TileContentWide = TileTemplateType.TileWide310x150BlockAndText01;
        private static readonly XmlDocument ContentWide = TileUpdateManager.GetTemplateContent(TileContentWide);
#endif
        private static readonly XmlDocument ContentSquare150 = TileUpdateManager.GetTemplateContent(TileContentSquare150);
        private static readonly XmlDocument ContentSquare310 = TileUpdateManager.GetTemplateContent(TileContentSquare310);


#endregion
    }
}