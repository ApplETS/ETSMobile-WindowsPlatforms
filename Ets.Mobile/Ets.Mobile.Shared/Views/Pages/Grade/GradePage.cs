using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Grade
{
    public partial class GradePage : IViewFor<GradePageViewModel>
    {
        #region IViewFor<T>

        public GradePageViewModel ViewModel
        {
            get { return (GradePageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(GradePageViewModel), typeof(GradePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (GradePageViewModel)value; }
        }

        #endregion

        public GradePage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel
                .BindTo(this, x => x.DataContext);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}