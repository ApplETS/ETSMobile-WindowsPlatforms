﻿using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Schedule;
using Windows.UI;
using Ets.Mobile.ViewModel;
using Ets.Mobile.ViewModel.Contracts.Shared;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.Pages.Schedule
{
    public sealed partial class SchedulePage : Page
    {
        partial void PartialInitialize()
        {
            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel
                .Subscribe(vm =>
                {
                    IsCommandBarVisible = new Subject<bool>();
                    vm.SideNavigation.IsSideNavigationVisibleSubject.Subscribe(x =>
                    {
                        CommandB.Visibility = x
                            ? Visibility.Collapsed
                            : Visibility.Visible;
                    });
                    IsCommandBarVisible
                        .Subscribe(x => CommandB.Visibility = x
                            ? Visibility.Visible
                            : Visibility.Collapsed);
                    IsCommandBarVisible.OnNext(true);
                });
        }

        public bool IsCurrentViewWeek { get; set; }
        public bool IsCurrentViewDay { get; set; }
        public bool IsCurrentViewMonth { get; set; }
        public bool IsCurrentViewTimeLine { get; set; }
        private ISubject<bool> IsCommandBarVisible { get; set; }

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
                if (CommandB != null && CommandB.Visibility == Visibility.Visible)
                {
                    IsCommandBarVisible.OnNext(false);
                }
            };

            // Show the app bar after the flyout closes
            calendarFlyout.Closed += (o, s) =>
            {
                if (CommandB != null && CommandB.Visibility == Visibility.Collapsed)
                {
                    IsCommandBarVisible.OnNext(true);
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
            Scheduler.ScheduleType = type;
            IsCurrentViewDay = type == ScheduleType.Day;
            IsCurrentViewWeek = type == ScheduleType.Week;
            IsCurrentViewMonth = type == ScheduleType.Month;
            IsCurrentViewTimeLine = type == ScheduleType.TimeLine;

            var calendarFlyout = (Flyout)Resources["ChangeCalendarViewFlyout"];
            calendarFlyout?.Hide();
        }

        private void schedule_ScheduleTypeChanging(object sender, ScheduleTypeChangingEventArgs e)
        {
            var sel = Scheduler?.Resources["TemplateSelector"] as AppointmentDataTemplateSelector;
            if (sel != null)
            {
                sel.Type = Scheduler.ScheduleType;
            }
        }

        private void schedule_Loaded(object sender, RoutedEventArgs e)
        {
            var sel = Scheduler?.Resources["TemplateSelector"] as AppointmentDataTemplateSelector;
            if (sel != null)
            {
                sel.Type = Scheduler.ScheduleType;
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
