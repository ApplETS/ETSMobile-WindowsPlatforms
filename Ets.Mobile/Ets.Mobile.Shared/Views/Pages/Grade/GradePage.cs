using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;

namespace Ets.Mobile.Pages.Grade
{
    public partial class GradePage : IViewFor<GradeViewModel>
    {
        #region IViewFor<T>

        public GradeViewModel ViewModel
        {
            get { return (GradeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(GradeViewModel), typeof(GradePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (GradeViewModel)value; }
        }

        #endregion

        public GradePage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel
                .Subscribe(x => Root.DataContext = x);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}
