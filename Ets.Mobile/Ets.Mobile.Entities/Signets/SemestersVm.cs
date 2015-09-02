using System;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
	public class SemesterVm : ReactiveObject
	{
		private string _AbridgedName;
		public string AbridgedName
		{ 
			get { return _AbridgedName; }
			set { this.RaiseAndSetIfChanged(ref _AbridgedName, value); }
		}

		private string _name;
		public string Name 
		{ 
			get { return _name; }
			set { this.RaiseAndSetIfChanged(ref _name, value); }
		}

		#region Date Range

		private DateTime _startDate;
		public DateTime StartDate
		{ 
			get { return _startDate; }
			set { this.RaiseAndSetIfChanged(ref _startDate, value); }
		}

		private DateTime _endDate;
		public DateTime EndDate
		{ 
			get { return _endDate; }
			set { this.RaiseAndSetIfChanged(ref _endDate, value); }
		}

        private DateTime _endOfClassesDate;
        public DateTime EndOfClassesDate
		{
            get { return _endOfClassesDate; }
            set { this.RaiseAndSetIfChanged(ref _endOfClassesDate, value); }
		}

		#endregion

		#region Cancellation With Reimbursement

		private DateTime _startCancellationDateWithReimbursement;
		public DateTime StartCancellationDateWithReimbursement
		{ 
			get { return _startCancellationDateWithReimbursement; }
			set { this.RaiseAndSetIfChanged(ref _startCancellationDateWithReimbursement, value); }
		}

		private DateTime _endCancellationDateWithReimbursement;
		public DateTime EndCancellationDateWithReimbursement
		{ 
			get { return _endCancellationDateWithReimbursement; }
			set { this.RaiseAndSetIfChanged(ref _endCancellationDateWithReimbursement, value); }
		}

		#endregion

		#region New Student - Cancellation With Reimbursement

		private DateTime _startCancellationDateWithReimbursementForNewStudent;
		public DateTime StartCancellationDateWithReimbursementForNewStudent
		{ 
			get { return _startCancellationDateWithReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _startCancellationDateWithReimbursementForNewStudent, value); }
		}

		private DateTime _endCancellationDateWithReimbursementForNewStudent;
		public DateTime EndCancellationDateWithReimbursementForNewStudent
		{ 
			get { return _endCancellationDateWithReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _endCancellationDateWithReimbursementForNewStudent, value); }
		}

		#endregion

		#region New Student - Cancellation Without Reimbursement

		private DateTime _startCancellationDateWithoutReimbursementForNewStudent;
		public DateTime StartCancellationDateWithoutReimbursementForNewStudent
		{ 
			get { return _startCancellationDateWithoutReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _startCancellationDateWithoutReimbursementForNewStudent, value); }
		}

		private DateTime _endCancellationDateWithoutReimbursementForNewStudent;
		public DateTime EndCancellationDateWithoutReimbursementForNewStudent
		{ 
			get { return _endCancellationDateWithoutReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _endCancellationDateWithoutReimbursementForNewStudent, value); }
		}

		#endregion

		private DateTime _limitDateForCancellingASEQ;
		public DateTime LimitDateForCancellingASEQ
		{ 
			get { return _limitDateForCancellingASEQ; }
			set { this.RaiseAndSetIfChanged(ref _limitDateForCancellingASEQ, value); }
		}
	}
}
