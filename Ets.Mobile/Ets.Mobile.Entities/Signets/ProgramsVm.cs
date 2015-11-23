using System;
using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Extensions;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class ProgramVm : ReactiveObject, IMergeableObject<ProgramVm>, IDisposable
    {
        #region IMergeableObject

        public bool Equals(ProgramVm x, ProgramVm y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(ProgramVm obj)
        {
            return obj.Name.GetHashCode();
        }

        public void MergeWith(ProgramVm other)
        {
            Code = other.Code;
            Status = other.Code;
            SemesterStart = other.SemesterStart;
            SemesterEnd = other.SemesterEnd;
            Average = other.Average;
            EquivalenceCount = other.EquivalenceCount;
            SuceededCreditsCount = other.SuceededCreditsCount;
            FailedCreditsCount = other.FailedCreditsCount;
            RegisteredCreditsCount = other.RegisteredCreditsCount;
            CompletedCreditsCount = other.CompletedCreditsCount;
            PotentialCreditsCount = other.PotentialCreditsCount;
            ResearchCreditsCount = other.ResearchCreditsCount;
        }

        #endregion

        private string _code;
        [DataMember]
        public string Code 
        { 
        	get { return _code; }
        	set { this.RaiseAndSetIfChanged(ref _code, value); }
        }

        private string _name;
        [DataMember]
        public string Name
        {
        	get { return _name; }
        	set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private string _status;
        [DataMember]
        public string Status
        { 
        	get { return _status; }
        	set { this.RaiseAndSetIfChanged(ref _status, value); }
        }

        private string _semesterStart;
        [DataMember]
        public string SemesterStart
        { 
        	get { return _semesterStart; }
        	set { this.RaiseAndSetIfChanged(ref _semesterStart, value); }
        }

        private string _semesterEnd;
        [DataMember]
        public string SemesterEnd 
        { 
        	get { return _semesterEnd; }
        	set { this.RaiseAndSetIfChanged(ref _semesterEnd, value); }
        }

        private string _average;
        [DataMember]
        public string Average
        { 
        	get { return _average; }
        	set { this.RaiseAndSetIfChanged(ref _average, value); }
        }

        private short _equivalenceCount;
        [DataMember]
        public short EquivalenceCount
        { 
        	get { return _equivalenceCount; }
        	set { this.RaiseAndSetIfChanged(ref _equivalenceCount, value); }
        }

        private short _suceededCreditsCount;
        [DataMember]
        public short SuceededCreditsCount
        { 
        	get { return _suceededCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _suceededCreditsCount, value); }
        }

        private short _failedCreditsCount;
        [DataMember]
        public short FailedCreditsCount
        { 
        	get { return _failedCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _failedCreditsCount, value); }
        }

        private short _registeredCreditsCount;
        [DataMember]
        public short RegisteredCreditsCount 
        { 
        	get { return _registeredCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _registeredCreditsCount, value); }
        }

        private short _completedCreditsCount;
        [DataMember]
        public short CompletedCreditsCount
        { 
        	get { return _completedCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _completedCreditsCount, value); }
        }

        private short _potentialCreditsCount;
        [DataMember]
        public short PotentialCreditsCount
        { 
        	get { return _potentialCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _potentialCreditsCount, value); }
        }

        private short _researchCreditsCount;
        [DataMember]
        public short ResearchCreditsCount 
        { 
        	get { return _researchCreditsCount; }
            set { this.RaiseAndSetIfChanged(ref _researchCreditsCount, value); }
        }

        public void Dispose() {}
    }
}