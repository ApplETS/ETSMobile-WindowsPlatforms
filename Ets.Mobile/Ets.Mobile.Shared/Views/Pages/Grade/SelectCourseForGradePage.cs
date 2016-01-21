using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Grade
{
    public partial class SelectCourseForGradePage : IViewFor<SelectCourseForGradePageViewModel>
    {
        #region IViewFor<T>

        public SelectCourseForGradePageViewModel ViewModel
        {
            get { return (SelectCourseForGradePageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SelectCourseForGradePageViewModel), typeof(SelectCourseForGradePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SelectCourseForGradePageViewModel)value; }
        }

        #endregion

        public SelectCourseForGradePage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadCoursesSummaries);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}