using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    public partial class MainPageViewModel
    {
        private void InitializeToday()
        {
            TodayItems = new ReactiveList<ScheduleVm>();

            LoadCoursesForToday = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchCoursesForTodayImpl());

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

        private IObservable<ScheduleVm[]> FetchCoursesForTodayImpl()
        {
            var fetchCoursesForToday =
                Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Semesters())
                    .Where(x => x.FirstOrDefault(y => y.StartDate <= DateTime.Now && y.EndDate > DateTime.Now) != null)
                    .Select(semesters => semesters.FirstOrDefault(y => y.StartDate <= DateTime.Now && y.EndDate > DateTime.Now))
                    .SelectMany(GetScheduleForSemester)
                    .Publish();

            fetchCoursesForToday.Connect();

            return fetchCoursesForToday.ThrowIfEmpty();
        } 

        private IObservable<ScheduleVm[]> GetScheduleForSemester(SemesterVm currentSemester)
        {
            return Cache.GetAndFetchLatest(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName),
                async () => await ClientServices().SignetsService.Schedule(currentSemester.AbridgedName).ApplyCustomColors(SettingsService()))
                .Select(courses => courses.Where(c => c.StartDate.Date == DateTime.Now.Date).ToArray());
        } 

        #region Properties

        [DataMember] public ReactiveList<ScheduleVm> TodayItems { get; set; }
        [DataMember] public IReactiveDerivedList<ScheduleVm> Today { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> TodayPresenter { get; protected set; }
        public ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; protected set; }

        #endregion
    }
}