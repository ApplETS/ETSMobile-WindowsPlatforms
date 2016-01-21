using Ets.Mobile.ViewModel.Contracts.Settings;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Content.Settings
{
    public sealed partial class Options : UserControl
    {
        public Options()
        {
            InitializeComponent();
            this.Events().DataContextChanged.Subscribe(args =>
            {
                var options = args.NewValue as IOptionsViewModel;
                if (options != null)
                {
                    options.HandleScheduleBackgroundService.IsExecuting.Subscribe(isExecuting =>
                    {
                        ShowSchedule.IsEnabled = !isExecuting;
                        LoadingShowScheduleChanged.Visibility = isExecuting ? Visibility.Visible : Visibility.Collapsed;
                    });

                    options.IntegrateScheduleToCalendar.IsExecuting.Subscribe(isIntegrating =>
                    {
                        RemoveScheduleFromCalendar.IsEnabled = !isIntegrating;
                    });

                    options.RemoveScheduleFromCalendar.IsExecuting.Subscribe(isRemovingIntegration =>
                    {
                        IntegrateScheduleToCalendar.IsEnabled = !isRemovingIntegration;
                    });
                }
            });
        }
    }
}