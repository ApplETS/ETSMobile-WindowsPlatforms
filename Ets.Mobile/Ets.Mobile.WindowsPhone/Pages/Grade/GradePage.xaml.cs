using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;

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

            subscriptionForViewModel.BindTo(this, x => x.DataContext);

            subscriptionForViewModel.Subscribe(x =>
            {
                IDisposable disposable = null;
                disposable = ViewModel.GradeItems.ItemsAdded.Subscribe(y =>
                {
                    RefreshGrade.Command = ViewModel.GradeItems[0].LoadGrade;
                    disposable?.Dispose();
                });
            });

            Pivot.SelectionChanged += (sender, e) =>
            {
                if (ViewModel?.GradeItems != null && ViewModel.GradeItems.Any())
                {
                    ChangeRefreshCommandAndExecuteOnce();
                }
            };
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
    }
}