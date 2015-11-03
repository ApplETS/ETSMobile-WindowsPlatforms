using System.Reactive;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using ReactiveUI;

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
            
            // Side Navigation
            // TODO: Move to Side Navigation UserControl
            SideViewButton.Command = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                SideViewButton.IsEnabled = false;
                SlideView.SelectedIndex = SlideView.SelectedIndex == 0 ? 1 : 0;
                BottomBar.Visibility = Visibility.Collapsed;
                return Observable.Return(Unit.Default);
            });

            SlideView.SelectionChanged += (sender, e) =>
            {
                BottomBar.Visibility = SlideView.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
                SideViewButton.IsEnabled = true;
            };

            // Handle the button visibility according to Pivot Context (SelectedIndex)
            Loaded += (s, e) =>
            {
                SlideView.SelectedIndex = 1;
                MainPivot.SelectionChanged += (sender, e2) =>
                {
                    RefreshToday.Visibility = Visibility.Collapsed;
                    RefreshGrade.Visibility = Visibility.Collapsed;
                    ShowSchedule.Visibility = Visibility.Collapsed;

                    switch (MainPivot.SelectedIndex)
                    {
                        case (int)MainPivotItem.Today:
                            RefreshToday.Visibility = Visibility.Visible;
                            ShowSchedule.Visibility = Visibility.Visible;
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
