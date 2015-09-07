using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.ViewModel.Pages.Schedule;
using ReactiveUI;

namespace Ets.Mobile.Pages.Schedule
{
    public partial class SchedulePage : Page, IViewFor<ScheduleViewModel>
    {
        #region IViewFor<T>

        public ScheduleViewModel ViewModel
        {
            get { return (ScheduleViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ScheduleViewModel), typeof(SchedulePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ScheduleViewModel)value; }
        }

        #endregion

        public SchedulePage()
        {
            this.InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadSchedule);

            this.OneWayBind(ViewModel, x => x.SchedulePresenter, x => x.Schedule.DataContext);
            this.OneWayBind(ViewModel, x => x.LoadSchedule, x => x.RefreshSchedule.Command);
        }
    }
}
