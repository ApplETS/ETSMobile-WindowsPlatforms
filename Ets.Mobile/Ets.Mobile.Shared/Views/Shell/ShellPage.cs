using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.ViewModel;
using Ets.Mobile.ViewModel.Pages.Main;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.Shell
{
    public partial class ShellPage : Page, IViewFor<ApplicationShell>
    {
        #region IViewFor<T>

        public ApplicationShell ViewModel
        {
            get { return (ApplicationShell)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ApplicationShell), typeof(ShellPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ApplicationShell)value; }
        }

        #endregion

        public ShellPage()
        {
            InitializeComponent();

            RxApp.SuspensionHost.ObserveAppState<ApplicationShell>()
                .ObserveOn(RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .BindTo(this, x => x.ViewModel);

            PartialInitialize();
        }

        partial void PartialInitialize();
    }
}
