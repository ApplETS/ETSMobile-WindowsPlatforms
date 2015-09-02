using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class EvaluationsVm : ReactiveObject
    {
        private double _actualGrade;
        [DataMember] public double ActualGrade
        { 
            get { return _actualGrade; } 
            set { this.RaiseAndSetIfChanged(ref _actualGrade, value); }
        }

        private double _finalGradeOnHundred;
        [DataMember] public double FinalGradeOnHundred
        {
            get { return _finalGradeOnHundred; }
            set { this.RaiseAndSetIfChanged(ref _finalGradeOnHundred, value); }
        }

        private double _averageOfClass;
        [DataMember] public double AverageOfClass 
        { 
            get { return _averageOfClass; }
            set { this.RaiseAndSetIfChanged(ref _averageOfClass, value); }
        }

        private double _standardDeviationOfClass;
        [DataMember] public double StandardDeviationOfClass 
        { 
            get { return _standardDeviationOfClass; }
            set { this.RaiseAndSetIfChanged(ref _standardDeviationOfClass, value); }
        }

        private double _medianOfClass;
        [DataMember] public double MedianOfClass 
        { 
            get { return _medianOfClass; }
            set { this.RaiseAndSetIfChanged(ref _medianOfClass, value); }
        }

        private double _percentileOfClass;
        [DataMember] public double PercentileOfClass 
        { 
            get { return _percentileOfClass; }
            set { this.RaiseAndSetIfChanged(ref _percentileOfClass, value); }
        }

        private double _actualGradeOfIndividualElements;
        [DataMember] public double ActualGradeOfIndividualElements 
        { 
            get { return _actualGradeOfIndividualElements; }
            set { this.RaiseAndSetIfChanged(ref _actualGradeOfIndividualElements, value); }
        }

        private double _gradeOnHundredOfIndividualElements;
        [DataMember] public double GradeOnHundredOfIndividualElements 
        { 
            get { return _gradeOnHundredOfIndividualElements; }
            set { this.RaiseAndSetIfChanged(ref _gradeOnHundredOfIndividualElements, value); }
        }

        [DataMember] public List<EvaluationVm> Evaluations { get; set; }
    }

    [DataContract]
    public class EvaluationVm : ReactiveObject
    {
        private string _courseAndGroup;
        [DataMember] public string CourseAndGroup 
        { 
            get { return _courseAndGroup; }
            set { this.RaiseAndSetIfChanged(ref _courseAndGroup, value); }
        }

        private string _name;
        [DataMember] public string Name 
        { 
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private string _team;
        [DataMember] public string Team 
        { 
            get { return _team; }
            set { this.RaiseAndSetIfChanged(ref _team, value); }
        }

        private DateTime? _targetDate;
        [DataMember] public DateTime? TargetDate 
        { 
            get { return _targetDate; }
            set { if(value != null) this.RaiseAndSetIfChanged(ref _targetDate, value); }
        }

        private double _grade;
        [DataMember] public double Grade 
        { 
            get { return _grade; }
            set { this.RaiseAndSetIfChanged(ref _grade, value); }
        }

        private string _total;
        [DataMember] public string Total 
        { 
            get { return _total; }
            set { this.RaiseAndSetIfChanged(ref _total, value); }
        }

        private string _weighting;
        [DataMember] public string Weighting 
        { 
            get { return _weighting; }
            set { this.RaiseAndSetIfChanged(ref _weighting, value); }
        }

        private string _average;
        [DataMember] public string Average 
        { 
            get { return _average; }
            set { this.RaiseAndSetIfChanged(ref _average, value); }
        }

        private string _standardDeviation;
        [DataMember] public string StandardDeviation 
        { 
            get { return _standardDeviation; }
            set { this.RaiseAndSetIfChanged(ref _standardDeviation, value); }
        }

        private string _median;
        [DataMember] public string Median 
        { 
            get { return _median; }
            set { this.RaiseAndSetIfChanged(ref _median, value); }
        }

        private string _percentile;
        [DataMember] public string Percentile 
        { 
            get { return _percentile; }
            set { this.RaiseAndSetIfChanged(ref _percentile, value); }
        }

        private string _published;
        [DataMember] public string Published 
        { 
            get { return _published; }
            set { this.RaiseAndSetIfChanged(ref _published, value); }
        }
        
        private string _messageOfTeacher;
        [DataMember] public string MessageOfTeacher 
        { 
            get { return _messageOfTeacher; }
            set { this.RaiseAndSetIfChanged(ref _messageOfTeacher, value); }
        }
        
        private bool _ignoredFromCalculation;
        [DataMember] public bool IgnoredFromCalculation 
        { 
            get { return _ignoredFromCalculation; }
            set { this.RaiseAndSetIfChanged(ref _ignoredFromCalculation, value); }
        }
    }
}