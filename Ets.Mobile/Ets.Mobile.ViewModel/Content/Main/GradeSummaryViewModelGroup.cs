using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeSummaryViewModelGroup : ReactiveObject, IGrouping<string, GradeSummaryViewModelItem>, IMergeableObject<GradeSummaryViewModelGroup>, IDisposable
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

        #region IMergeableObject

        public bool Equals(GradeSummaryViewModelGroup x, GradeSummaryViewModelGroup y)
        {
            return x.Key == y.Key;
        }

        public int GetHashCode(GradeSummaryViewModelGroup obj)
        {
            return obj.Key.GetHashCode();
        }

        public void MergeWith(GradeSummaryViewModelGroup other)
        {
            GradesItems.MergeWith(other.GradesItems);
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            GradesItems?.Clear();
        }

        #endregion

        public GradeSummaryViewModelGroup(string semester, IEnumerable<CourseVm> courses, ReactiveCommand<GradeSummaryViewModelItem> navigateToGradeCommand)
        {
            // Semester
            Semester = semester;

            // Key
            Key = semester;

            // Courses
            GradesItems = new ReactiveList<GradeSummaryViewModelItem>();
            GradesItems.AddRange(courses.Select(y => new GradeSummaryViewModelItem(semester, y, navigateToGradeCommand)));
        }

        #region Properties

        private string _semester;
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private ReactiveList<GradeSummaryViewModelItem> _gradesItems;
        public ReactiveList<GradeSummaryViewModelItem> GradesItems
        {
            get { return _gradesItems; }
            set { this.RaiseAndSetIfChanged(ref _gradesItems, value); }
        }

        #endregion
    }
}