using Ets.Mobile.ViewModel.Pages.Main;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Pages.Main
{
    public partial class MainPage : Page, IViewFor<MainPageViewModel>
    {
        #region IViewFor<T>

        public MainPageViewModel ViewModel
        {
            get { return (MainPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MainPageViewModel), typeof(MainPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainPageViewModel)value; }
        }

        #endregion

        public enum MainPivotItem
        {
            Today = 0,
            Grade = 1
        }

        public MainPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadCoursesForToday);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadCoursesSummaries);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}