using Ets.Mobile.Client.Entities.Schedule;
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
                var tileManager = TileUpdateManager.CreateTileUpdaterForApplication();
                tileManager.Clear();
                foreach (var tile in tileManager.GetScheduledTileNotifications())
                {
                    tileManager.RemoveFromSchedule(tile);
                }
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

        public static IAsyncAction RunTaskAsync()
        {
            return Task.Run(async () => {
                var scheduleItem =
                    await LiveTileAndLockScreenService.GetCurrentOrIncomingCourses();

                if (scheduleItem != null && scheduleItem.Any())
                {
                    ScheduleTiles(scheduleItem.ToArray());
                }
                else
                {
                    TileUpdateManager.CreateTileUpdaterForApplication().Clear();
                    var scheduleItemForNextDay =
                        await LiveTileAndLockScreenService.GetFollowingDayCourses();
                    if (scheduleItemForNextDay.Any())
                    {
                        ScheduleFirstTileOfNextDay(scheduleItemForNextDay.First());
                    }
                }
            }).AsAsyncAction();
        }

        private static void ScheduleTiles(ScheduleForLiveTile[] schedule)
        {
            var now = DateTimeOffset.Now;

            // Tile Update Manager
            //
            var tileManager = TileUpdateManager.CreateTileUpdaterForApplication();
            tileManager.Clear();

            if (!schedule.Any(t => t.EndDate > now))
            {
                foreach (var tile in tileManager.GetScheduledTileNotifications())
                {
                    tileManager.RemoveFromSchedule(tile);
                }
                return;
            }
            
            tileManager.EnableNotificationQueue(true);
            tileManager.EnableNotificationQueueForSquare150x150(true);
            tileManager.EnableNotificationQueueForWide310x150(true);

            // Schedule Notifications
            //
            var scheduleItems = schedule.Where(t => t.EndDate > now).ToArray();

            // We take 1 to 2 schedule items (2 schedule items X 2 Tile Sizes = 4
            // and the maximum is 5 tiles scheduled)
            scheduleItems = scheduleItems.Length > 1 ? scheduleItems.Take(2).ToArray() : scheduleItems;

            if (scheduleItems.Length == 1)
            {
                // We have only one class right now or later in the day
                var tile = scheduleItems[0];
                var tileNotification = GenerateTileNotifications(tile);
                tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item1, DateTimeOffset.Now.AddSeconds(2))
                {
                    ExpirationTime = tile.EndDate
                });
                tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item2, DateTimeOffset.Now.AddSeconds(3))
                {
                    ExpirationTime = tile.EndDate
                });
            }
            else
            {
                // We have at least two classes, but since there are limitations, we only take 2.
                if (scheduleItems.GroupBy(x => new { x.ActivityName, x.StartDate, x.EndDate }).ToArray().Length == 1)
                {
                    // They are the exact same class, starting at the same time.
                    // This may occur if the user has (for instance) 2 different labs and 
                    // the user needs to pick a class at the start of the semester.
                    // Note: We have a second between each deletion
                    foreach (var tile in scheduleItems)
                    {
                        var tileNotification = GenerateTileNotifications(tile);
                        tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item1, DateTimeOffset.Now.AddSeconds(2))
                        {
                            ExpirationTime = tile.EndDate.Add(new TimeSpan(0, 0, 0, -15))
                        });
                        tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item2, DateTimeOffset.Now.AddSeconds(3))
                        {
                            ExpirationTime = tile.EndDate.Add(new TimeSpan(0, 0, 0, -15))
                        });
                    }

                    // Clear both classes
                    // There seem to be a limitation to remove two 
                    var clear = GenerateTileNotifications(scheduleItems[0]);
                    tileManager.AddToSchedule(new ScheduledTileNotification(clear.Item1, scheduleItems[0].EndDate.Add(new TimeSpan(0, 0, 0, -15)))
                    {
                        ExpirationTime = scheduleItems[0].EndDate
                    });
                }
                else
                {
                    // They are two different classes
                    var tile = scheduleItems[0];
                    var tileNotification = GenerateTileNotifications(tile);
                        
                    // The class is occuring right now and the tile isn't showing
                    // the schedule. Schedule it right now.
                    //
                    // Similarly, if it is the first class, 
                    // we schedule it to show right now (because we want to see which
                    // classes we have next)
                    tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item1, DateTimeOffset.Now.AddSeconds(2))
                    {
                        ExpirationTime = tile.EndDate
                    });
                    tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item2, DateTimeOffset.Now.AddSeconds(2))
                    {
                        ExpirationTime = tile.EndDate
                    });
                    var dateUntilNextClass = tile.EndDate.AddMilliseconds(1);

                    // the class is occuring later in the day (after the first one), so we get the previous class
                    // and schedule its appearance when the previous class finishes
                    tile = schedule[1];
                    tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item1, dateUntilNextClass)
                    {
                        ExpirationTime = tile.EndDate
                    });
                    tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item2, dateUntilNextClass)
                    {
                        ExpirationTime = tile.EndDate
                    });
                }
            }
        }

        private static void ScheduleFirstTileOfNextDay(ScheduleForLiveTile tile)
        {
            var now = DateTimeOffset.Now;

            // Tile Update Manager
            //
            var tileManager = TileUpdateManager.CreateTileUpdaterForApplication();

            // Schedule Notifications
            //
            var scheduledNotifications = tileManager.GetScheduledTileNotifications();
            var tileId = $"{tile.ActivityName}{tile.Location} - {tile.Name} (Gr: {tile.Group}){tile.StartDate.ToString("HH:mm tt")} - {tile.EndDate.ToString("HH:mm tt")}";

            if (!scheduledNotifications.Any(sn => ScheduleIsScheduled(tileId, sn)))
            {
                var tommorow = now.Date.AddDays(1).AddMilliseconds(1);
                var tileNotification = GenerateTileNotifications(tile);

                tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item1, tommorow)
                {
                    ExpirationTime = tile.EndDate
                });
                tileManager.AddToSchedule(new ScheduledTileNotification(tileNotification.Item2, tommorow)
                {
                    ExpirationTime = tile.EndDate
                });
            }
        }

        /// <summary>
        /// Generate the tile for a ScheduleVm
        /// </summary>
        /// <param name="tile">ScheduleVm as ScheduleForLiveTile</param>
        /// <returns>T1 is 150x150 Live Tile, T2 is Wide Live Tile</returns>
        private static Tuple<XmlDocument, XmlDocument> GenerateTileNotifications(ScheduleForLiveTile tile)
        {
            var tileId = $"{tile.ActivityName}{tile.Location} - {tile.Name} (Gr: {tile.Group}){tile.StartDate.ToString("HH:mm tt")} - {tile.EndDate.ToString("HH:mm tt")}";
#if WINDOWS_PHONE_APP
            var contentForLockScreenWide = TileUpdateManager.GetTemplateContent(TileForLockScreenWide);
#else
            var contentWide = TileUpdateManager.GetTemplateContent(TileContentWide);
#endif
            var contentSquare150 = TileUpdateManager.GetTemplateContent(TileContentSquare150);
#if WINDOWS_PHONE_APP
            // Wide
            //
            var idOfNotification = contentForLockScreenWide.CreateAttribute(IdOfNotificationKey);
            idOfNotification.InnerText = $"{tile.ActivityName}{tile.Location}{tile.StartDate.ToString("HH:mm tt")} - {tile.EndDate.ToString("HH:mm tt")}";
            foreach (var item in contentForLockScreenWide.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                {

                    switch (namedItem.NodeValue.ToString())
                    {
                        case "1":
                            item.InnerText = tile.ActivityName;
                            break;
                        case "2":
                            item.InnerText = $"{tile.Location} - {tile.Name} (Gr: {tile.Group})";
                            break;
                        case "3":
                            item.InnerText = tile.StartDate.ToString("HH:mm tt") + " - " + tile.EndDate.ToString("HH:mm tt");
                            break;
                    }
                }
            }
            foreach (var item in contentForLockScreenWide.SelectNodes("//image"))
            {
                var namedItem = item.Attributes.GetNamedItem("src");
                if (namedItem != null)
                    namedItem.InnerText = "/Assets/TileImage/Badge.scale-240.png";
            }
#else
            // Wide
            //
            var idOfNotification = contentWide.CreateAttribute(IdOfNotificationKey);
            idOfNotification.InnerText = tileId;
            foreach (var item in contentWide.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                {

                    switch (namedItem.NodeValue.ToString())
                    {
                        case "3":
                            item.InnerText = tile.ActivityName;
                            break;
                        case "4":
                            item.InnerText = $"{tile.Location} - {tile.Name} (Gr: {tile.Group})";
                            break;
                        case "5":
                            item.InnerText = tile.StartDate.ToString("HH:mm tt") + " - " + tile.EndDate.ToString("HH:mm tt");
                            break;
                    }
                }
            }
#endif
            // Square
            //
            var contentSquare150Id = contentSquare150.CreateAttribute(IdOfNotificationKey);
            contentSquare150Id.InnerText = tileId;
            foreach (var item in contentSquare150.SelectNodes("//text"))
            {
                var namedItem = item.Attributes.GetNamedItem("id");
                if (namedItem != null)
                    switch (namedItem.NodeValue.ToString())
                    {
                        case "1":
                            item.InnerText = tile.ActivityName;
                            break;
                        case "2":
                            item.InnerText = $"{tile.Location}"
                                + "\n" + tile.StartDate.ToString("HH:mm tt") + " - " + tile.EndDate.ToString("HH:mm tt");
                            break;
                    }
            }
            
            return new Tuple<XmlDocument, XmlDocument>(
#if WINDOWS_PHONE_APP
                contentForLockScreenWide,
#else
                contentWide,
#endif

                contentSquare150);
        }

        #region Properties

        public static string TaskName { get; } = "ScheduTileUpdater";
        private const string TaskEntryPoint = "Ets.Mobile.Agent.ScheduleTileUpdaterBackgroundTask";

        private const string IdOfNotificationKey = "id_of_notification";
        private static readonly Func<string, ScheduledTileNotification, bool> ScheduleIsScheduled = (tileId, x) => x.Content?.InnerText == tileId;
        private const TileTemplateType TileContentSquare150 = TileTemplateType.TileSquare150x150Text02;
#if WINDOWS_PHONE_APP
        private const TileTemplateType TileForLockScreenWide = TileTemplateType.TileWide310x150IconWithBadgeAndText;
#else
        private const TileTemplateType TileContentWide = TileTemplateType.TileWide310x150BlockAndText01;
#endif

        #endregion
    }
}