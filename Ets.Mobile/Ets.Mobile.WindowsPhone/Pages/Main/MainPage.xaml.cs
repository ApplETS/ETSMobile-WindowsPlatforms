using System.Reactive.Linq;
using Windows.UI.Xaml;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Splat;

namespace Ets.Mobile.Pages.Main
{
    public sealed partial class MainPage
    {
        partial void PartialInitialize()
        {
            // Ensure to hide the status bar
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            statusBar.BackgroundOpacity = 0;
            statusBar.HideAsync().GetResults();

            // Grade Presenter
            // NOTE: Do not remove this code, can't bind to the presenter's source
            this.OneWayBind(ViewModel, x => x.GradesPresenter, x => x.Grade.DataContext);
            
            // Handle the button visibility according to Pivot Context (SelectedIndex)
            Loaded += (s, e) =>
            {
                //SlideView.SelectedIndex = 1;
                MainPivot.SelectionChanged += (sender, e2) =>
                {
                    RefreshToday.Visibility = Visibility.Collapsed;
                    RefreshGrade.Visibility = Visibility.Collapsed;

                    switch (MainPivot.SelectedIndex)
                    {
                        case (int)MainPivotItem.Today:
                            RefreshToday.Visibility = Visibility.Visible;
                            break;
                        case (int)MainPivotItem.Grade:
                            RefreshGrade.Visibility = Visibility.Visible;
                            break;
                    }
                };
            };
        }
    }
}
