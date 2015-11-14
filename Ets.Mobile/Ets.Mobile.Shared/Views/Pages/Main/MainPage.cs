using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.ViewModel.Pages.Main;
using ReactiveUI;

namespace Ets.Mobile.Pages.Main
{
    public partial class MainPage : Page, IViewFor<MainViewModel>
    {
        #region IViewFor<T>

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(MainPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainViewModel)value; }
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
                .InvokeCommand(this, x => x.ViewModel.LoadGrades);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}
