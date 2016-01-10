using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Grade
{
    public partial class SelectCourseForGradePage : IViewFor<SelectCourseForGradeViewModel>
    {
        #region IViewFor<T>

        public SelectCourseForGradeViewModel ViewModel
        {
            get { return (SelectCourseForGradeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SelectCourseForGradeViewModel), typeof(SelectCourseForGradePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SelectCourseForGradeViewModel)value; }
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