using System;
using Windows.UI.Xaml;

namespace Ets.Mobile.Pages.Main
{
    public sealed partial class MainPage
    {
        partial void PartialInitialize()
        {
            // Ensure to hide the status bar
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                // Ensure to hide the status bar
                var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 0;
                statusBar.HideAsync().GetResults();
            }

            // Handle the button visibility according to Pivot Context (SelectedIndex)
            this.Events().Loaded.Subscribe(e =>
            {
                MainPivot.SelectionChanged += (sender, e2) =>
                {
                    RefreshToday.Visibility = Visibility.Collapsed;
                    RefreshGrade.Visibility = Visibility.Collapsed;

                    switch (MainPivot.SelectedIndex)
                    {
                        case (int) MainPivotItem.Today:
                            RefreshToday.Visibility = Visibility.Visible;
                            break;
                        case (int) MainPivotItem.Grade:
                            RefreshGrade.Visibility = Visibility.Visible;
                            break;
                    }
                };
            });
        }
    }
}