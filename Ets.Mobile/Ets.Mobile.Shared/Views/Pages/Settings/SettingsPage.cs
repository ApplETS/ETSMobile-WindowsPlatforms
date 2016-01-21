using Ets.Mobile.ViewModel.Pages.Settings;
using ReactiveUI;
using System.Reactive.Linq;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Settings
{
    public partial class SettingsPage : IViewFor<SettingsPageViewModel>
    {
        #region IViewFor<T>

        public SettingsPageViewModel ViewModel
        {
            get { return (SettingsPageViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof (SettingsPageViewModel), typeof (SettingsPage),
                new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SettingsPageViewModel) value; }
        }

        #endregion

        public SettingsPage()
        {
            InitializeComponent();

            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel.BindTo(this, x => x.DataContext);
        }
    }
}