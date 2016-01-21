using Ets.Mobile.ViewModel.Pages.Program;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Program
{
    public partial class ProgramPage : IViewFor<ProgramPageViewModel>
    {
        #region IViewFor<T>

        public ProgramPageViewModel ViewModel
        {
            get { return (ProgramPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ProgramPageViewModel), typeof(ProgramPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ProgramPageViewModel)value; }
        }

        #endregion

        public ProgramPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.FetchPrograms);
        }
    }
}