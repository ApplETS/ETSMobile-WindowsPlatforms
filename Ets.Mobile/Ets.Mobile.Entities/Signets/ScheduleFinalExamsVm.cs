using System;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    public class ScheduleFinalExamVm : ReactiveObject
    {
    	private string _abridged;
        public string Abridged
        {
        	get { return _abridged; }
        	set { this.RaiseAndSetIfChanged(ref _abridged, value); }
        }

        private string _group;
        public string Group
        {
        	get { return _group; }
            set { this.RaiseAndSetIfChanged(ref _group, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
        	get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }

        private TimeSpan _startHour;
        public TimeSpan StartHour
        {
        	get { return _startHour; }
            set { this.RaiseAndSetIfChanged(ref _startHour, value); }
        }

        private TimeSpan _endHour;
        public TimeSpan EndHour
        {
        	get { return _endHour; }
            set { this.RaiseAndSetIfChanged(ref _endHour, value); }
        }

        private string _location;
        public string Location
        {
        	get { return _location; }
            set { this.RaiseAndSetIfChanged(ref _location, value); }
        }
    }
}
