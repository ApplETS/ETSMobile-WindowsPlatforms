using Ets.Mobile.ViewModel.Contracts.Grade;
using System.Collections.Generic;
using Ets.Mobile.ViewModel.Content.Main;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI.Xaml.Controls.Core;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class SelectCourseForGradeViewModelDt : DesignTimeBase, ISelectCourseForGradeViewModel
    {
        public SelectCourseForGradeViewModelDt()
        {
            GradesItems = new ReactiveList<GradeSummaryViewModelGroup>();
            LoadGrades = ReactivePresenterCommand.CreateAsyncObservable(_ => { return Observable.Return(default(List<GradeSummaryViewModelGroup>)); });
        }

        private ReactiveList<GradeSummaryViewModelGroup> _gradeItems;
        public ReactiveList<GradeSummaryViewModelGroup> GradesItems {
            get { return _gradeItems; }
            set { _gradeItems = value; OnPropertyChanged(); }
        }

        public ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; }
    }
}