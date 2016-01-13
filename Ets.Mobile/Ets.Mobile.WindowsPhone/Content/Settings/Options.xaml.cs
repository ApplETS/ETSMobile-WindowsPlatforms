using Ets.Mobile.ViewModel.Contracts.Settings;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ReactiveUI;
using System.Reactive.Concurrency;

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
                }
            });
        }
    }
}