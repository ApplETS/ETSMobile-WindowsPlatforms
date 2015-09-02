using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    public class ProgramVm : ReactiveObject
    {
    	private string _code;
        public string Code 
        { 
        	get { return _code; }
        	set { this.RaiseAndSetIfChanged(ref _code, value); }
        }

        private string _name;
        public string Name
        {
        	get { return _name; }
        	set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private string _status;
        public string Status
        { 
        	get { return _status; }
        	set { this.RaiseAndSetIfChanged(ref _status, value); }
        }

        private string _semesterStart;
        public string SemesterStart
        { 
        	get { return _semesterStart; }
        	set { this.RaiseAndSetIfChanged(ref _semesterStart, value); }
        }

        private string _semesterEnd;
        public string SemesterEnd 
        { 
        	get { return _semesterEnd; }
        	set { this.RaiseAndSetIfChanged(ref _semesterEnd, value); }
        }

        private double _average;
        public double Average
        { 
        	get { return _average; }
        	set { this.RaiseAndSetIfChanged(ref _average, value); }
        }

        private string _equivalenceCount;
        public string EquivalenceCount
        { 
        	get { return _equivalenceCount; }
        	set { this.RaiseAndSetIfChanged(ref _equivalenceCount, value); }
        }

        private string _suceededCreditsCount;
        public string SuceededCreditsCount
        { 
        	get { return _suceededCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _suceededCreditsCount, value); }
        }

        private string _failedCreditsCount;
        public string FailedCreditsCount
        { 
        	get { return _failedCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _failedCreditsCount, value); }
        }

        private string _registeredCreditsCount;
        public string RegisteredCreditsCount 
        { 
        	get { return _registeredCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _registeredCreditsCount, value); }
        }

        private string _completedCreditsCount;
        public string CompletedCreditsCount
        { 
        	get { return _completedCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _completedCreditsCount, value); }
        }

        private string _potentialCreditsCount;
        public string PotentialCreditsCount
        { 
        	get { return _potentialCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _potentialCreditsCount, value); }
        }

        private string _searchCreditsCount;
        public string SearchCreditsCount 
        { 
        	get { return _searchCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _searchCreditsCount, value); }
        }
    }
}
