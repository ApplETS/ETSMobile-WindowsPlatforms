using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.ViewModel.Content.Schedule
{
    public class ScheduleGroupedByDayViewModelItem : ReactiveObject, IGrouping<string, ScheduleVm>, IDisposable
    {
        #region IGrouping<string, ScheduleVm>

        public IEnumerator<ScheduleVm> GetEnumerator()
        {
            return ScheduleItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Key { get; }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            ScheduleItems = null;
        }

        #endregion
        
        public ScheduleGroupedByDayViewModelItem(DateTime key, IEnumerable<ScheduleVm> vm)
        {
            Key = key.ToString("D");
            Date = key;
            ScheduleItems = new ReactiveList<ScheduleVm>(vm);
        }

        #region Properties

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }

        private ReactiveList<ScheduleVm> _scheduleItems;
        public ReactiveList<ScheduleVm> ScheduleItems
        {
            get { return _scheduleItems; }
            set { this.RaiseAndSetIfChanged(ref _scheduleItems, value); }
        }

        #endregion
    }
}