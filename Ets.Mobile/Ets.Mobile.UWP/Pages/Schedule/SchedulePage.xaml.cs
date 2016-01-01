using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Ets.Mobile.Pages.Schedule
{
    public sealed partial class SchedulePage : Page
    {
        partial void PartialInitialize()
        {
        }

        public bool IsCurrentViewWeek { get; set; }
        public bool IsCurrentViewDay { get; set; }
        public bool IsCurrentViewMonth { get; set; }
        public bool IsCurrentViewTimeLine { get; set; }

        private void ChangeCalendarView_Click(object sender, RoutedEventArgs e)
        {
            // Ensure we have an app bar
            if (CommandB == null) return;

            // Get the button just clicked
            var changeViewButton = sender as AppBarButton;
            if (changeViewButton == null) return;

            // Get the attached flyout
            var calendarFlyout = (Flyout)Resources["ChangeCalendarViewFlyout"];
            if (calendarFlyout == null) return;

            // Close the app bar before opening the flyout
            calendarFlyout.Opening += (o, s) =>
            {
                if (CommandB != null && CommandB.Visibility == Visibility.Visible)
                {
                    CommandB.Visibility = Visibility.Collapsed;
                }
            };

            // Show the app bar after the flyout closes
            calendarFlyout.Closed += (o, s) =>
            {
                if (CommandB != null && CommandB.Visibility == Visibility.Collapsed)
                {
                    CommandB.Visibility = Visibility.Visible;
                }
            };

            var grid = calendarFlyout.Content as Grid;
            var stackPanel = grid?.Children.First() as StackPanel;
            stackPanel?.Children.ForEach(x =>
            {
                var b = x as Button;
                if (b != null)
                {
                    b.IsEnabled = b.Name != Scheduler.ScheduleType.ToString();
                    b.Foreground = b.IsEnabled ? Application.Current.RequestedTheme == ApplicationTheme.Light ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Gray);
                }
            });

            if (grid == null) return;
            grid.Tapped += (o, args) =>
            {
                var transparentGrid = args.OriginalSource as Grid;
                if (transparentGrid != null)
                {
                    calendarFlyout.Hide();
                }
            };

            // Use the ShowAt() method on the flyout to specify where exactly the flyout should be located
            calendarFlyout.ShowAt(CommandB);
        }

        private void ChangeToDay(object sender, RoutedEventArgs e)
        {
            SetViewForScheduler(ScheduleType.Day);
        }

        private void ChangeToWeek(object sender, RoutedEventArgs e)
        {
            SetViewForScheduler(ScheduleType.Week);
        }

        private void ChangeToMonth(object sender, RoutedEventArgs e)
        {
            SetViewForScheduler(ScheduleType.Month);
        }

        private void ChangeToTimeLine(object sender, RoutedEventArgs e)
        {
            SetViewForScheduler(ScheduleType.TimeLine);
        }

        private void SetViewForScheduler(ScheduleType type)
        {
            if (Scheduler.ScheduleType == type)
            {
                return;
            }
            CommandB.Visibility = Visibility.Visible;

            var calendarFlyout = (Flyout)Resources["ChangeCalendarViewFlyout"];
            calendarFlyout?.Hide();

            Scheduler.ScheduleType = type;
            IsCurrentViewDay = type == ScheduleType.Day;
            IsCurrentViewWeek = type == ScheduleType.Week;
            IsCurrentViewMonth = type == ScheduleType.Month;
            IsCurrentViewTimeLine = type == ScheduleType.TimeLine;
        }

        private void schedule_ScheduleTypeChanging(object sender, ScheduleTypeChangingEventArgs e)
        {
            var sel = Resources["TemplateSelector"] as AppointmentDataTemplateSelector;
            if (sel != null)
            {
                sel.Type = Scheduler.ScheduleType;
            }
        }

        private void schedule_Loaded(object sender, RoutedEventArgs e)
        {
            var sel = Resources["TemplateSelector"] as AppointmentDataTemplateSelector;
            if (sel != null)
            {
                sel.Type = Scheduler.ScheduleType;
            }
        }

        private readonly TimeSpan _removeTimeSpanForVisibility = new TimeSpan(0, 30, 0); 
        private void Scheduler_OnVisibleDatesChanging(object sender, VisibleDatesChangingEventArgs e)
        {
            var dates = e.NewValue as IEnumerable<DateTime>;
            var datesArray = dates?.ToArray();
            if (datesArray?.Length == 1)
            {
                var scheduleForDayOrTimeline = (from app in Scheduler.Appointments where app.StartTime.Date == datesArray.First().Date select app).OrderBy(a => a.StartTime).ToList();
                if (scheduleForDayOrTimeline.Any())
                {
                    Scheduler.MoveToTime(scheduleForDayOrTimeline.First().StartTime.TimeOfDay.Subtract(_removeTimeSpanForVisibility));
                }
            }
        }
    }

    public class AppointmentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TimelineViewTemplate { get; set; }

        public DataTemplate DayViewTemplate { get; set; }

        public DataTemplate WeekViewTemplate { get; set; }

        public DataTemplate MonthViewTemplate { get; set; }

        public ScheduleType Type { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            DataTemplate appointmentTemplate;

            switch (Type)
            {
                case ScheduleType.Month:
                    appointmentTemplate = MonthViewTemplate;
                    break;
                case  ScheduleType.Day:
                    appointmentTemplate = DayViewTemplate;
                    break;
                case ScheduleType.TimeLine:
                    appointmentTemplate = TimelineViewTemplate;
                    break;
                case ScheduleType.Week:
                    appointmentTemplate = WeekViewTemplate;
                    break;
                case ScheduleType.WorkWeek:
                    appointmentTemplate = WeekViewTemplate;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var frameworkElement = appointmentTemplate?.LoadContent() as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.DataContext = item;
            }

            return appointmentTemplate;
        }
    }
}