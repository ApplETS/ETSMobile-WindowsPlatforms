using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Moodle.Courses
{
    public partial class MoodleCourseModulePage : IViewFor<MoodleCourseModulePageViewModel>
    {
        #region IViewFor<T>

        public MoodleCourseModulePageViewModel ViewModel
        {
            get { return (MoodleCourseModulePageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MoodleCourseModulePageViewModel), typeof(MoodleCourseModulePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MoodleCourseModulePageViewModel)value; }
        }

        #endregion

        public MoodleCourseModulePage()
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