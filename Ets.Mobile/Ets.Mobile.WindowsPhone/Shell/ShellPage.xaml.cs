using System.Reactive.Linq;
using ReactiveUI;
using System;
using Windows.UI.Xaml;

namespace Ets.Mobile.Shell
{
    public sealed partial class ShellPage
    {
        partial void PartialInitialize()
        {
            this.Bind(ViewModel, x => x.SideNavigation.IsSideNavigationVisible, x => x.ShellSplitView.IsPaneOpen);
            this.Bind(ViewModel, x => x.SideNavigation, x => x.SideNavigation.DataContext);
            this.Bind(ViewModel, x => x.Router, x => x.RoutedViewModelHost.Router);
        }
    }
}