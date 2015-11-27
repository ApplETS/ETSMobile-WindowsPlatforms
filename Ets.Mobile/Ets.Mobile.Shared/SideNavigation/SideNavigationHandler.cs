using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.ViewModel.Bases;

namespace Ets.Mobile.SideNavigation
{
    public class SideNavigationHandler
    {
        private static int count = 0;
        public static Action<AppBar, ViewModelBase> HandleForAppBar = (appBar, vm) =>
        {
            if (appBar != null && count == 0)
            {
                appBar.Visibility = Visibility.Visible;
                vm.SideNavigation.IsSideNavigationVisibleSubject.Subscribe(
                    isVisible =>
                    {
                        appBar.Visibility = isVisible ? Visibility.Collapsed : Visibility.Visible;
                    });
                count++;
            }
        };
    }
}