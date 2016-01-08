using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Contracts.Grade;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class SelectCourseForGradeViewModelDt : DesignTimeBase, ISelectCourseForGradeViewModel
    {
        public SelectCourseForGradeViewModelDt()
        {
            GradesItems = new ReactiveList<GradeSummaryViewModelGroup>();
            LoadCoursesSummaries = ReactivePresenterCommand.CreateAsyncObservable(_ => { return Observable.Return(default(List<GradeSummaryViewModelGroup>)); });
        }

        private ReactiveList<GradeSummaryViewModelGroup> _gradeItems;
        public ReactiveList<GradeSummaryViewModelGroup> GradesItems {
            get { return _gradeItems; }
            set { _gradeItems = value; OnPropertyChanged(); }
        }

        public ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; }
    }
}