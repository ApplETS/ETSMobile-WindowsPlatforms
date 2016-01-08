﻿using Akavache;
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
    public partial class MainViewModel
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
            return Cache.GetAndFetchLatest(ViewModelKeys.Semesters, () => ClientServices().SignetsService.Semesters())
                    .Where(x => x.FirstOrDefault(y => y.StartDate <= DateTime.Now && y.EndDate > DateTime.Now) != null)
                    .Select(semesters => semesters.FirstOrDefault(y => y.StartDate <= DateTime.Now && y.EndDate > DateTime.Now))
                    .SelectMany(GetScheduleForSemester)
                    .ThrowIfEmpty();
        } 

        private IObservable<ScheduleVm[]> GetScheduleForSemester(SemesterVm currentSemester)
        {
            return Cache.GetAndFetchLatest(ViewModelKeys.ScheduleForSemester(currentSemester.AbridgedName),
                async () =>
                    await
                        ClientServices()
                            .SignetsService.Schedule(currentSemester.AbridgedName)
                            .ApplyCustomColors(SettingsService()));
        } 

        #region Properties

        [DataMember] public ReactiveList<ScheduleVm> TodayItems { get; protected set; }
        [DataMember] public IReactiveDerivedList<ScheduleVm> Today { get; protected set; }
        public IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> TodayPresenter { get; protected set; }
        public ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; protected set; }

        #endregion
    }
}