using System.Reactive.Linq;
using Windows.UI.Xaml;
using Ets.Mobile.ViewModel.Pages.Program;
using ReactiveUI;

namespace Ets.Mobile.Pages.Program
{
    public partial class ProgramPage : IViewFor<ProgramViewModel>
    {
        #region IViewFor<T>

        public ProgramViewModel ViewModel
        {
            get { return (ProgramViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ProgramViewModel), typeof(ProgramPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ProgramViewModel)value; }
        }

        #endregion

        public ProgramPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadProgram);
        }
    }
}
