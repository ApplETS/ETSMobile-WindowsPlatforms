using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class EvaluationsVm : ReactiveObject, ICustomColor
    {
        private string _letterGrade;
        [DataMember]
        public string LetterGrade
        {
            get { return _letterGrade; }
            set { this.RaiseAndSetIfChanged(ref _letterGrade, value); }
        }

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

        private double _average;
        [DataMember] public double Average 
        { 
            get { return _average; }
            set { this.RaiseAndSetIfChanged(ref _average, value); }
        }

        private double _standardDeviation;
        [DataMember] public double StandardDeviation 
        { 
            get { return _standardDeviation; }
            set { this.RaiseAndSetIfChanged(ref _standardDeviation, value); }
        }

        private double _median;
        [DataMember] public double Median 
        { 
            get { return _median; }
            set { this.RaiseAndSetIfChanged(ref _median, value); }
        }

        private double _percentile;
        [DataMember] public double Percentile 
        { 
            get { return _percentile; }
            set { this.RaiseAndSetIfChanged(ref _percentile, value); }
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



        #region ICustomColor Implementation

        [DataMember]
        public byte A { get; set; }
        [DataMember]
        public byte R { get; set; }
        [DataMember]
        public byte G { get; set; }
        [DataMember]
        public byte B { get; set; }

        public void SetNewColor(ColorVm color)
        {
            // Set Value for Store
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;

            // Set Value For Evaluations
            foreach (var eval in Evaluations)
            {
                eval.SetNewColor(color);
            }
        }

        public SolidColorBrush Brush => new SolidColorBrush(Color.FromArgb(A, R, G, B));

        #endregion
    }

    [DataContract]
    public class EvaluationVm : ReactiveObject, ICustomColor
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

        private double _total;
        [DataMember] public double Total 
        { 
            get { return _total; }
            set { this.RaiseAndSetIfChanged(ref _total, value); }
        }

        [DataMember]
        public double GradeComputed => Math.Round((_grade / _total) * 100, 2, MidpointRounding.AwayFromZero);

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

        #region ICustomColor Implementation

        [DataMember]
        public byte A { get; set; }
        [DataMember]
        public byte R { get; set; }
        [DataMember]
        public byte G { get; set; }
        [DataMember]
        public byte B { get; set; }

        public void SetNewColor(ColorVm color)
        {
            // Set Value for Store
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public SolidColorBrush Brush => new SolidColorBrush(Color.FromArgb(A, R, G, B));

        #endregion
    }
}