using Ets.Mobile.ViewModel.Pages.Moodle;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Moodle
{
    public partial class MoodleMainPage : IViewFor<MoodleMainPageViewModel>
    {
        #region IViewFor<T>

        public MoodleMainPageViewModel ViewModel
        {
            get { return (MoodleMainPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MoodleMainPageViewModel), typeof(MoodleMainPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MoodleMainPageViewModel)value; }
        }

        #endregion

        public MoodleMainPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);
            
            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadCourses);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}