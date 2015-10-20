using System.Reactive;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using ReactiveUI;
using Themes;

namespace Ets.Mobile.Pages.Main
{
    public sealed partial class MainPage
    {
        partial void PartialInitialize()
        {
            //var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            //statusBar.BackgroundColor = AppBrushes.HighBrush;
            //statusBar.BackgroundOpacity = 1;

            // Today Presenter
            this.OneWayBind(ViewModel, x => x.TodayPresenter, x => x.Today.DataContext);
            this.OneWayBind(ViewModel, x => x.LoadCoursesForToday, x => x.RefreshToday.Command);

            // Grade Presenter
            this.OneWayBind(ViewModel, x => x.GradesPresenter, x => x.Grade.DataContext);
            this.OneWayBind(ViewModel, x => x.LoadGrades, x => x.RefreshGrade.Command);

            // Side Navigation
            //this.OneWayBind(ViewModel, x => x.Profile, x => x.SideNavigation.ProfileDataContext);
            //this.OneWayBind(ViewModel, x => x.LoadProfile, x => x.SideNavigation.RefreshProfileCommand);
            //this.OneWayBind(ViewModel, x => x.Logout, x => x.SideNavigation.LogoutCommand);
            //this.OneWayBind(ViewModel, x => x.NavigateToUserDetails, x => x.SideNavigation.ProfileTappedCommand);

            // Show Schedule
            this.OneWayBind(ViewModel, x => x.NavigateToSchedule, x => x.ShowSchedule.Command);

            //SideViewButton.Command = ReactiveCommand.CreateAsyncObservable(_ =>
            //{
            //    SideViewButton.IsEnabled = false;
            //    SlideView.SelectedIndex = SlideView.SelectedIndex == 0 ? 1 : 0;
            //    BottomBar.Visibility = Visibility.Collapsed;
            //    return Observable.Return(Unit.Default);
            //});

            //SlideView.SelectionChanged += (sender, e) =>
            //{
            //    BottomBar.Visibility = SlideView.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
            //    SideViewButton.IsEnabled = true;
            //};

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
                        case (int) MainPivotItem.Today:
                            RefreshToday.Visibility = Visibility.Visible;
                            ShowSchedule.Visibility = Visibility.Visible;
                            break;
                        case (int) MainPivotItem.Grade:
                            RefreshGrade.Visibility = Visibility.Visible;
                            break;
                    }
                };
            };
        }
    }
}
