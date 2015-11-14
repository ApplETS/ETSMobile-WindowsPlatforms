using System;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    public class ReplacedDayVm : ReactiveObject
    {
    	private DateTime _originDate;
        public DateTime OriginDate
        { 
        	get { return _originDate; }
        	set { this.RaiseAndSetIfChanged(ref _originDate, value); } 
        }

        private DateTime _targetDate;
        public DateTime TargetDate
        { 
        	get { return _targetDate; }
        	set { this.RaiseAndSetIfChanged(ref _targetDate, value); } 
        }

        private string _description;
        public string Description
        { 
        	get { return _description; }
        	set { this.RaiseAndSetIfChanged(ref _description, value); } 
        }
    }
}
