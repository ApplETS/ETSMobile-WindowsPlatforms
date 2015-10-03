using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ReactiveUI;
using System;

namespace Ets.Mobile.Pages.Grade
{
    public sealed partial class GradePage : Page
    {
        partial void PartialInitialize()
        {
            var subscriptionForViewModel = this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null);

            subscriptionForViewModel
                .InvokeCommand(this, x => x.ViewModel.LoadGrade);

            subscriptionForViewModel
                .Subscribe(x =>
                {
                    x.LoadGrade.Subscribe(y =>
                    {
                        ChangeRefreshCommandAndExecuteOnce();
                    });
                });

            Pivot.SelectionChanged += (sender, e) =>
            {
                if (ViewModel?.GradeItems != null && ViewModel.GradeItems.Any())
                {
                    ChangeRefreshCommandAndExecuteOnce();
                }
            };

            this.OneWayBind(ViewModel, x => x.InitialIndex, x => x.Pivot.SelectedIndex);

            this.OneWayBind(ViewModel, x => x.Grades, x => x.Pivot.ItemsSource);
        }

        private void ChangeRefreshCommandAndExecuteOnce()
        {
            if (ViewModel?.GradeItems != null && ViewModel.GradeItems.Any())
            {
                RefreshGrade.Command = ViewModel.GradeItems[Pivot.SelectedIndex].LoadGrade;
                if (!ViewModel.GradeItems[Pivot.SelectedIndex].HasTriggeredLoadGradeOnce)
                {
                    RefreshGrade.Command?.Execute(null);
                    ViewModel.GradeItems[Pivot.SelectedIndex].HasTriggeredLoadGradeOnce = true;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
