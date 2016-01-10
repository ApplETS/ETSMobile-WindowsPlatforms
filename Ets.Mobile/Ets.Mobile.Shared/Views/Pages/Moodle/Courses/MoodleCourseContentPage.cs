using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Moodle.Courses
{
    public partial class MoodleCourseContentPage : IViewFor<MoodleCourseContentPageViewModel>
    {
        #region IViewFor<T>

        public MoodleCourseContentPageViewModel ViewModel
        {
            get { return (MoodleCourseContentPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MoodleCourseContentPageViewModel), typeof(MoodleCourseContentPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MoodleCourseContentPageViewModel)value; }
        }

        #endregion

        public MoodleCourseContentPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadCoursesContent);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}