using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ReactiveUI;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Schedule;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.UI;
using Windows.UI.Xaml.Markup;
using Ets.Mobile.SyncFusion.SfSchedule;

namespace Ets.Mobile.Pages.Schedule
{
    public sealed partial class SchedulePage : Page
    {
        partial void PartialInitialize()
        {
            var reactiveToCollection = new ReactiveListScheduleToSynfusionSchedulerConverter();
            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel
                .Subscribe(x =>
                {
                    x.ScheduleItems.Changed.Subscribe(y =>
                    {
                        Scheduler.Appointments.Clear();
                        Scheduler.Appointments = (ScheduleAppointmentCollection)reactiveToCollection.Convert(x.ScheduleItems, x.ScheduleItems.GetType(), "", "");
                    });
                });
        }

        public bool IsCurrentViewWeek { get; set; }
        public bool IsCurrentViewDay { get; set; }
        public bool IsCurrentViewMonth { get; set; }
        public bool IsCurrentViewTimeLine { get; set; }

        private void ChangeCalendarView_Click(object sender, RoutedEventArgs e)
        {
            // Ensure we have an app bar
            if (BottomAppBar == null) return;

            // Get the button just clicked
            var changeViewButton = sender as AppBarButton;
            if (changeViewButton == null) return;

            // Get the attached flyout
            var calendarFlyout = (Flyout)Resources["ChangeCalendarViewFlyout"];
            if (calendarFlyout == null) return;

            // Close the app bar before opening the flyout
            calendarFlyout.Opening += (o, s) =>
            {
                if (BottomAppBar != null && BottomAppBar.Visibility == Visibility.Visible)
                {
                    BottomAppBar.Visibility = Visibility.Collapsed;
                }
            };

            // Show the app bar after the flyout closes
            calendarFlyout.Closed += (o, s) =>
            {
                if (BottomAppBar != null && BottomAppBar.Visibility == Visibility.Collapsed)
                {
                    BottomAppBar.Visibility = Visibility.Visible;
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
                    b.Foreground = b.IsEnabled ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Gray);
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
            calendarFlyout.ShowAt(BottomAppBar);
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
            Scheduler.ScheduleType = type;
            IsCurrentViewDay = type == ScheduleType.Day;
            IsCurrentViewWeek = type == ScheduleType.Week;
            IsCurrentViewMonth = type == ScheduleType.Month;
            IsCurrentViewTimeLine = type == ScheduleType.TimeLine;

            var calendarFlyout = (Flyout)Resources["ChangeCalendarViewFlyout"];
            calendarFlyout?.Hide();
        }
    }
}
