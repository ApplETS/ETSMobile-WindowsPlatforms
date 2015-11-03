using System;
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

        private string _average;
        public string Average
        { 
        	get { return _average; }
        	set { this.RaiseAndSetIfChanged(ref _average, value); }
        }

        private short _equivalenceCount;
        public short EquivalenceCount
        { 
        	get { return _equivalenceCount; }
        	set { this.RaiseAndSetIfChanged(ref _equivalenceCount, value); }
        }

        private short _suceededCreditsCount;
        public short SuceededCreditsCount
        { 
        	get { return _suceededCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _suceededCreditsCount, value); }
        }

        private short _failedCreditsCount;
        public short FailedCreditsCount
        { 
        	get { return _failedCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _failedCreditsCount, value); }
        }

        private short _registeredCreditsCount;
        public short RegisteredCreditsCount 
        { 
        	get { return _registeredCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _registeredCreditsCount, value); }
        }

        private short _completedCreditsCount;
        public short CompletedCreditsCount
        { 
        	get { return _completedCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _completedCreditsCount, value); }
        }

        private short _potentialCreditsCount;
        public short PotentialCreditsCount
        { 
        	get { return _potentialCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _potentialCreditsCount, value); }
        }

        private short _researchCreditsCount;
        public short ResearchCreditsCount 
        { 
        	get { return _researchCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _researchCreditsCount, value); }
        }
    }
}
