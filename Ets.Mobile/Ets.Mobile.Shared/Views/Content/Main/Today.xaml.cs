using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Main;
using ReactiveUI;

namespace Ets.Mobile.Views.Content.Main
{
    public sealed partial class Today : UserControl, IViewFor<MainViewModel>
    {
        public Today()
        {
            InitializeComponent();
        }

        #region IViewFor<T>

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(MainPage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainViewModel)value; }
        }

        #endregion
    }
}
