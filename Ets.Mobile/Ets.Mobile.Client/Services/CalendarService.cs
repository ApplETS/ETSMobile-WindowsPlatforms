using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
#if WINDOWS_UWP || WINDOWS_PHONE_APP
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Resources;
#endif

namespace Ets.Mobile.Client.Services
{
    public class CalendarService : ICalendarService, IEnableLogger
    {
        private const string CalendarIdStoreKey = "schedule_integration_calendar_localid";

        public async Task IntegrateScheduleToCalendar(ScheduleVm[] currentOrNextSemesterSchedule)
        {
#if WINDOWS_PHONE_APP || WINDOWS_UWP
            var store = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AppCalendarsReadWrite);

            // Delete all existing calendar that is named CalendarName
            try
            {
                var localId = await BlobCache.UserAccount.GetObject<string>(CalendarIdStoreKey).ToTask();
                if (!string.IsNullOrEmpty(localId))
                {
                    var calendarToRemove = await store.GetAppointmentCalendarAsync(localId);
                    if (calendarToRemove != null)
                    {
                        await calendarToRemove.DeleteAsync();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                this.Log().Debug("The calendar probably didn't exist: " + ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                this.Log().Debug("The calendar didn't exist: " + ex.Message);
            }

            // Create calendar with appointments
            var calendar = await store.CreateAppointmentCalendarAsync(Locator.Current.GetService<ResourceLoader>().GetString("ScheduleCalendarName"));
            await BlobCache.UserAccount.InsertObject(CalendarIdStoreKey, calendar.LocalId).ToTask();
            
            foreach (var scheduleItem in currentOrNextSemesterSchedule)
            {
                var roamingId = scheduleItem.CourseAndGroup + ";" + scheduleItem.StartDate.ToString("O") + ";" + scheduleItem.EndDate.ToString("O") + ";" + scheduleItem.Location;

                // Create
                var appToStore = new Appointment
                    {

                        AllDay = false,
                        AllowNewTimeProposal = false,
                        BusyStatus = AppointmentBusyStatus.Busy,
                        Location = scheduleItem.Location,
                        Duration = scheduleItem.EndDate - scheduleItem.StartDate,
                        IsCanceledMeeting = false,
                        IsResponseRequested = false,
                        IsOrganizedByUser = false,
                        StartTime = scheduleItem.StartDate,
                        Subject = scheduleItem.ActivityName,
                        RoamingId = roamingId,
                        Details = $"{scheduleItem.Name} (Gr: {scheduleItem.Group})"
                    };

                await calendar.SaveAppointmentAsync(appToStore);
            }
#endif
        }

        public async Task<Tuple<bool, string>> RemoveScheduleFromCalendar()
        {
#if WINDOWS_PHONE_APP || WINDOWS_UWP
            var store = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AppCalendarsReadWrite);

            string localId;
            try
            {
                localId = await BlobCache.UserAccount.GetObject<string>(CalendarIdStoreKey).ToTask();
            }
            catch (KeyNotFoundException)
            {
                return new Tuple<bool, string>(false, "key_not_found");
            }
            
            if (!string.IsNullOrEmpty(localId))
            {
                var calendarToRemove = await store.GetAppointmentCalendarAsync(localId);
                if (calendarToRemove != null)
                {
                    await calendarToRemove.DeleteAsync();
                }
            }
            await BlobCache.UserAccount.Invalidate(CalendarIdStoreKey).ToTask();
#endif
            return new Tuple<bool, string>(true, string.Empty);
        }
    }
}