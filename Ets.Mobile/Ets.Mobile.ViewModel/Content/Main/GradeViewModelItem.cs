using System;
using System.Reactive.Linq;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeViewModelItem : ReactiveObject, IDisposable
    {
        private IDisposable _disposable;

        private CourseVm _course;
        public CourseVm Course {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private string _semester;
        public string Semester {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private EvaluationsVm _evaluations;
        public EvaluationsVm Evaluations {
            get { return _evaluations; }
            set { this.RaiseAndSetIfChanged(ref _evaluations, value); }
        }

        #region Computed

        private string _gradeSummary;
        public string GradeSummary {
            get { return _gradeSummary; }
            set { this.RaiseAndSetIfChanged(ref _gradeSummary, value); }
        }

        #endregion

        public GradeViewModelItem(string semester, CourseVm course)
        {
            Semester = semester;
            Course = course;
        }

        public void FetchGrades(IObservable<EvaluationsVm> evaluationObservable)
        {
            _disposable = evaluationObservable.ObserveOn(RxApp.MainThreadScheduler).Subscribe(evals => {
                Evaluations = evals;
                if(string.IsNullOrEmpty(Course.Grade) && (int)Evaluations.ActualGrade == 0)
                {
                    GradeSummary = "";
                }
                else if (Course.Grade == "S")
                {
                    GradeSummary = "S";
                }
                else if (Course.Grade.Contains("L") && (int)Evaluations.ActualGrade == 0)
                {
                    GradeSummary = "E";
                }
                else if (Course.Grade.Contains("I") && (int)Evaluations.ActualGrade == 0)
                {
                    GradeSummary = "I";
                }
                else
                {
                    GradeSummary = Evaluations.ActualGrade.ToString();
                }
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
