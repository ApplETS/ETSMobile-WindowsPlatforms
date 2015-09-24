using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeSummaryViewModelGroup : ReactiveObject, IGrouping<string, GradeSummaryViewModelItem>, IDisposable
    {
        #region IGrouping<string, GradesViewModelItem>

        public string Key { get; set; }

        public IEnumerator<GradeSummaryViewModelItem> GetEnumerator()
        {
            return _gradesItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _gradesItems.GetEnumerator();
        }

        #endregion

        private string _semester;
        public string Semester {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private ReactiveList<GradeSummaryViewModelItem> _gradesItems;
        public ReactiveList<GradeSummaryViewModelItem> GradesItems {
            get { return _gradesItems; }
            set { this.RaiseAndSetIfChanged(ref _gradesItems, value); }
        }

        public GradeSummaryViewModelGroup(string semester, IEnumerable<CourseVm> courses, ReactiveCommand<Unit> navigateToGradeCommand)
        {
            // Semester
            Semester = semester;

            // Key
            Key = semester;

            // Courses
            GradesItems = new ReactiveList<GradeSummaryViewModelItem>();
            GradesItems.AddRange(courses.Select(y => new GradeSummaryViewModelItem(semester, y, navigateToGradeCommand)));
        }

        public void Dispose()
        {
            GradesItems?.Clear();
        }
    }
}
