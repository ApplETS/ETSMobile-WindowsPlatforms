using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Main
{
    [DataContract]
    public class ScheduleTileViewModel : ReactiveObject, IDisposable
    {
        [DataMember]
        public ScheduleVm Model { get; protected set; }

        public ScheduleTileViewModel(ScheduleVm model)
        {
            Model = model;
            _timeRemainingDisposable = new CompositeDisposable();
            IsTimeRemainingVisible = false;

            // TODO Make it work!
            // BindTimeRemaining();
        }

        private readonly CompositeDisposable _timeRemainingDisposable;

        #region IDisposable Implementation

        public void Dispose()
        {
            _timeRemainingDisposable?.Dispose();
        }

        #endregion

        #region Time Remaining

        private string _timeRemaining;
        public string TimeRemaining
        {
            get { return _timeRemaining; }
            set { this.RaiseAndSetIfChanged(ref _timeRemaining, value); }
        }

        private bool _isTimeRemainingVisible;
        public bool IsTimeRemainingVisible
        {
            get { return _isTimeRemainingVisible; }
            set { this.RaiseAndSetIfChanged(ref _isTimeRemainingVisible, value); }
        }

        public void BindTimeRemaining()
        {
            var timer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMinutes(1));

            var showTimeRemaining =
                timer.Where(x => Model.StartDate.TimeOfDay > DateTime.Now.TimeOfDay && (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes > 0 && (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes < 60)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
                {
                    TimeRemaining = (Model.StartDate.TimeOfDay.Minutes - DateTime.Now.TimeOfDay.Minutes).ToString();
                    IsTimeRemainingVisible = true;
                });
            _timeRemainingDisposable.Add(showTimeRemaining);

            var hideTimeRemaining =
                timer.Where(x => Model.StartDate.TimeOfDay < DateTime.Now.TimeOfDay && (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).Minutes < 0 || (Model.StartDate.TimeOfDay - DateTime.Now.TimeOfDay).Minutes > 60)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
                {
                    if (IsTimeRemainingVisible)
                    {
                        IsTimeRemainingVisible = false;
                    }
                });
            _timeRemainingDisposable.Add(hideTimeRemaining);
        }

        #endregion
    }
}
