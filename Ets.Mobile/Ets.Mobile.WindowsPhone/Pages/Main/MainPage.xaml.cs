using System;
using Windows.UI.Xaml;
using EventsMixin = Windows.UI.Xaml.Controls.EventsMixin;

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
            
            // Handle the button visibility according to Pivot Context (SelectedIndex)
            this.Events().Loaded.Subscribe(e =>
            {
                EventsMixin.Events(MainPivot).SelectionChanged.Subscribe(e2 =>
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
                });
            });
        }
    }
}