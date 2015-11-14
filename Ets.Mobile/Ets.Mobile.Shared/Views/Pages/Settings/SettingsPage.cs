using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Windows.UI.Xaml;
using Ets.Mobile.ViewModel.Pages.Settings;
using ReactiveUI;

namespace Ets.Mobile.Pages.Settings
{
    public partial class SettingsPage : IViewFor<SettingsViewModel>
    {
        #region IViewFor<T>

        public SettingsViewModel ViewModel
        {
            get { return (SettingsViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof (SettingsViewModel), typeof (SettingsPage),
                new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SettingsViewModel) value; }
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
