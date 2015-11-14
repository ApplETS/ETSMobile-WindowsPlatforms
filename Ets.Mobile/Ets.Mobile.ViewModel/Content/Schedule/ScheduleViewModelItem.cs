using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Schedule
{
    public class ScheduleViewModelItem : ReactiveObject, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            Schedule = null;
        }

        #endregion

        private ScheduleVm _schedule;
        [DataMember]
        public ScheduleVm Schedule
        {
            get { return _schedule; }
            set { this.RaiseAndSetIfChanged(ref _schedule, value); }
        }

        public ScheduleViewModelItem(ScheduleVm vm)
        {
            Schedule = vm;
        }
    }
}
