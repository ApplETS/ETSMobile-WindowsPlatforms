using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Extensions;
using System.Reactive;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeSummaryViewModelItem : ReactiveObject, IMergeableObject<GradeSummaryViewModelItem>
    {
        #region IMergeableObject

        public bool Equals(GradeSummaryViewModelItem x, GradeSummaryViewModelItem y)
        {
            return x.Course?.Acronym == y.Course?.Acronym
                && x.Semester == y.Semester;
        }

        public int GetHashCode(GradeSummaryViewModelItem obj)
        {
            return obj.Course.Acronym.GetHashCode() ^ obj.Semester.GetHashCode();
        }

        public void MergeWith(GradeSummaryViewModelItem other)
        {
            Course.MergeWith(other.Course);
            Semester = other.Semester;
        }

        #endregion

        public GradeSummaryViewModelItem(string semester, CourseVm course, ReactiveCommand<GradeSummaryViewModelItem> command)
        {
            Semester = semester;
            Course = course;
            if (Course.Semester != "s.o." && Course.Semester != "N/A")
            {
                NavigateToGrade = command;
            }
        }

        #region Properties

        private CourseVm _course;
        public CourseVm Course
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

        public ReactiveCommand<GradeSummaryViewModelItem> NavigateToGrade { get; protected set; }

        #endregion
    }
}