using Ets.Mobile.Entities.Moodle;
using ReactiveUI;
using ReactiveUI.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Ets.Mobile.ViewModel.Content.Moodle.Courses
{
    public class MoodleCourseSummaryViewModelGroup : ReactiveObject, IGrouping<string, MoodleCourseSummaryViewModelItem>, IMergeableObject<MoodleCourseSummaryViewModelGroup>, IDisposable
    {
        #region IGrouping<string, GradesViewModelItem>

        public string Key { get; set; }

        public IEnumerator<MoodleCourseSummaryViewModelItem> GetEnumerator()
        {
            return _coursesItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _coursesItems.GetEnumerator();
        }

        #endregion

        #region IMergeableObject

        public bool Equals(MoodleCourseSummaryViewModelGroup x, MoodleCourseSummaryViewModelGroup y)
        {
            return x.Key == y.Key;
        }

        public int GetHashCode(MoodleCourseSummaryViewModelGroup obj)
        {
            return obj.Key.GetHashCode();
        }

        public void MergeWith(MoodleCourseSummaryViewModelGroup other)
        {
            CoursesItems.MergeWith(other.CoursesItems);
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            CoursesItems?.Clear();
        }

        #endregion

        public MoodleCourseSummaryViewModelGroup(string semester, IEnumerable<MoodleCourseVm> courses, ReactiveCommand<Unit> navigateToCourseCommand)
        {
            // Semester
            Semester = semester;

            // Key
            Key = semester;

            // Courses
            CoursesItems = new ReactiveList<MoodleCourseSummaryViewModelItem>();
            CoursesItems.AddRange(courses.Select(y => new MoodleCourseSummaryViewModelItem(semester, y, navigateToCourseCommand)));
        }

        #region Properties

        private string _semester;
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private ReactiveList<MoodleCourseSummaryViewModelItem> _coursesItems;
        public ReactiveList<MoodleCourseSummaryViewModelItem> CoursesItems
        {
            get { return _coursesItems; }
            set { this.RaiseAndSetIfChanged(ref _coursesItems, value); }
        }

        #endregion
    }
}