using Ets.Mobile.ViewModel.Pages.Schedule;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Pages.Schedule
{
    public partial class SchedulePage : Page, IViewFor<SchedulePageViewModel>
    {
        #region IViewFor<T>

        public SchedulePageViewModel ViewModel
        {
            get { return (SchedulePageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SchedulePageViewModel), typeof(SchedulePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SchedulePageViewModel)value; }
        }

        #endregion

        public SchedulePage()
        {
            InitializeComponent();
            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.FetchSchedule);
            
            this.OneWayBind(ViewModel, x => x.FetchSchedule, x => x.RefreshSchedule.Command);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}