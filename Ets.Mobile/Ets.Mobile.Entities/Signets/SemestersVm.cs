using ReactiveUI;
using System;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
	public class SemesterVm : ReactiveObject
	{
		private string _abridgedName;
		[DataMember] public string AbridgedName
		{ 
			get { return _abridgedName; }
			set { this.RaiseAndSetIfChanged(ref _abridgedName, value); }
		}

		private string _name;
		[DataMember] public string Name 
		{ 
			get { return _name; }
			set { this.RaiseAndSetIfChanged(ref _name, value); }
		}

		#region Date Range

		private DateTime _startDate;
		[DataMember] public DateTime StartDate
		{ 
			get { return _startDate; }
			set { this.RaiseAndSetIfChanged(ref _startDate, value); }
		}

		private DateTime _endDate;
		[DataMember] public DateTime EndDate
		{ 
			get { return _endDate; }
			set { this.RaiseAndSetIfChanged(ref _endDate, value); }
		}

        private DateTime _endOfClassesDate;
        [DataMember] public DateTime EndOfClassesDate
		{
            get { return _endOfClassesDate; }
            set { this.RaiseAndSetIfChanged(ref _endOfClassesDate, value); }
		}

		#endregion

		#region Cancellation With Reimbursement

		private DateTime _startCancellationDateWithReimbursement;
		[DataMember] public DateTime StartCancellationDateWithReimbursement
		{ 
			get { return _startCancellationDateWithReimbursement; }
			set { this.RaiseAndSetIfChanged(ref _startCancellationDateWithReimbursement, value); }
		}

		private DateTime _endCancellationDateWithReimbursement;
		[DataMember] public DateTime EndCancellationDateWithReimbursement
		{ 
			get { return _endCancellationDateWithReimbursement; }
			set { this.RaiseAndSetIfChanged(ref _endCancellationDateWithReimbursement, value); }
		}

		#endregion

		#region New Student - Cancellation With Reimbursement

		private DateTime _startCancellationDateWithReimbursementForNewStudent;
		[DataMember] public DateTime StartCancellationDateWithReimbursementForNewStudent
		{ 
			get { return _startCancellationDateWithReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _startCancellationDateWithReimbursementForNewStudent, value); }
		}

		private DateTime _endCancellationDateWithReimbursementForNewStudent;
		[DataMember] public DateTime EndCancellationDateWithReimbursementForNewStudent
		{ 
			get { return _endCancellationDateWithReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _endCancellationDateWithReimbursementForNewStudent, value); }
		}

		#endregion

		#region New Student - Cancellation Without Reimbursement

		private DateTime _startCancellationDateWithoutReimbursementForNewStudent;
		[DataMember] public DateTime StartCancellationDateWithoutReimbursementForNewStudent
		{ 
			get { return _startCancellationDateWithoutReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _startCancellationDateWithoutReimbursementForNewStudent, value); }
		}

		private DateTime _endCancellationDateWithoutReimbursementForNewStudent;
		[DataMember] public DateTime EndCancellationDateWithoutReimbursementForNewStudent
		{ 
			get { return _endCancellationDateWithoutReimbursementForNewStudent; }
			set { this.RaiseAndSetIfChanged(ref _endCancellationDateWithoutReimbursementForNewStudent, value); }
		}

		#endregion

		private DateTime _limitDateForCancellingAseq;
		[DataMember] public DateTime LimitDateForCancellingAseq
		{ 
			get { return _limitDateForCancellingAseq; }
			set { this.RaiseAndSetIfChanged(ref _limitDateForCancellingAseq, value); }
		}
	}
}