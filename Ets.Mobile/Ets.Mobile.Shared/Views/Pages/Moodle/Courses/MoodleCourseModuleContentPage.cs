using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Moodle.Courses
{
    public partial class MoodleCourseModuleContentPage : IViewFor<MoodleCourseModuleContentPageViewModel>
    {
        #region IViewFor<T>

        public MoodleCourseModuleContentPageViewModel ViewModel
        {
            get { return (MoodleCourseModuleContentPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MoodleCourseModuleContentPageViewModel), typeof(MoodleCourseModuleContentPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MoodleCourseModuleContentPageViewModel)value; }
        }

        #endregion

        public MoodleCourseModuleContentPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);
            
            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}