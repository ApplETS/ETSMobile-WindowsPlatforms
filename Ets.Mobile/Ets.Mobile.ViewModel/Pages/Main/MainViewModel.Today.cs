using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainViewModel
    {
        private void InitializeToday()
        {
            TodayItems = new ReactiveList<ScheduleVm>();

            LoadCoursesForToday = ReactivePresenterCommand.CreateAsyncObservable(_ =>
            {
                return Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Semesters())
                    .Where(x => x != null && x.Any(y => !string.IsNullOrEmpty(y.AbridgedName)))
                    .ThrowIfEmpty()
                    .SelectMany(x => x)
                    .FirstAsync(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                    .ThrowIfEmpty()
                    .SelectMany(currentSemester => Cache.GetAndFetchLatest(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName), async () => {
                        var schedule = await ClientServices().SignetsService.Schedule(currentSemester.AbridgedName);
                        await SettingsService().ApplyColorOnItemsForSemester(schedule, currentSemester.AbridgedName, x => x.Title);
                        return schedule;
                    }))
                    .Where(x => x != null)
                    .Select(x => x)
                    .ThrowIfEmpty();
            });

            LoadCoursesForToday.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            Today = TodayItems.CreateDerivedCollection(
                x => x,
                x => x.Dispose(),
                x => x.StartDate.Date.Equals(DateTime.Now.Date),
                (x, y) => TimeSpan.Compare(x.StartDate.TimeOfDay, y.StartDate.TimeOfDay));

            TodayPresenter = LoadCoursesForToday.CreateReactivePresenter(TodayItems, Today, true);
        }

        #region Properties

        [DataMember] public ReactiveList<ScheduleVm> TodayItems { get; protected set; }
        [DataMember] public IReactiveDerivedList<ScheduleVm> Today { get; protected set; }
        public IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> TodayPresenter { get; protected set; }
        public ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; protected set; }
        private readonly ReplaySubject<Exception> _scheduleExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}