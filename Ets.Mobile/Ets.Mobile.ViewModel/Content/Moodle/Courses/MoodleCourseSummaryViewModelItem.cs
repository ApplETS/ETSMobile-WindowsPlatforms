using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using ReactiveUI;
using ReactiveUI.Extensions;

namespace Ets.Mobile.ViewModel.Content.Moodle.Courses
{
    public class MoodleCourseSummaryViewModelItem : ReactiveObject, IMergeableObject<MoodleCourseSummaryViewModelItem>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseSummaryViewModelItem x, MoodleCourseSummaryViewModelItem y)
        {
            return x.Course?.Id == y.Course?.Id
                && x.Semester == y.Semester;
        }

        public int GetHashCode(MoodleCourseSummaryViewModelItem obj)
        {
            return obj.Course.Id.GetHashCode() ^ obj.Semester.GetHashCode();
        }

        public void MergeWith(MoodleCourseSummaryViewModelItem other)
        {
            Course.MergeWith(other.Course);
            Semester = other.Semester;
        }

        #endregion

        public MoodleCourseSummaryViewModelItem(string semester, MoodleCourseVm course, ReactiveCommand<MoodleCourseContentPageViewModel> navigateToCourse)
        {
            Semester = semester;
            Course = course;
            NavigateToCourse = navigateToCourse;
        }

        #region Properties

        private MoodleCourseVm _course;
        public MoodleCourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private string _semester;
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        public ReactiveCommand<MoodleCourseContentPageViewModel> NavigateToCourse { get; protected set; }

        #endregion
    }
}