using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using ReactiveUI;
using ReactiveUI.Extensions;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Content.Moodle.Courses.Content
{
    [DataContract]
    public class MoodleCourseContentSummaryViewModel : ReactiveObject, IMergeableObject<MoodleCourseContentSummaryViewModel>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseContentSummaryViewModel x, MoodleCourseContentSummaryViewModel y)
        {
            return x.CourseContent.Id == y.CourseContent.Id;
        }

        public int GetHashCode(MoodleCourseContentSummaryViewModel obj)
        {
            return obj.CourseContent.Id.GetHashCode();
        }

        public void MergeWith(MoodleCourseContentSummaryViewModel other)
        {
            CourseContent.MergeWith(other.CourseContent);
            Course.MergeWith(other.Course);
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            NavigateToCourseModule = null;
            CourseContent = null;
            Course = null;
        }

        #endregion

        public MoodleCourseContentSummaryViewModel(MoodleCourseVm course, MoodleCourseContentVm courseContent, ReactiveCommand<MoodleCourseModulePageViewModel> navigateToCourseModule)
        {
            Course = course;
            CourseContent = courseContent;
            NavigateToCourseModule = navigateToCourseModule;
        }

        #region Properties

        private MoodleCourseVm _course;
        [DataMember]
        public MoodleCourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private MoodleCourseContentVm _courseContent;
        [DataMember]
        public MoodleCourseContentVm CourseContent
        {
            get { return _courseContent; }
            set { this.RaiseAndSetIfChanged(ref _courseContent, value); }
        }

        private ReactiveCommand<MoodleCourseModulePageViewModel> _navigateToCourseModule;
        public ReactiveCommand<MoodleCourseModulePageViewModel> NavigateToCourseModule
        {
            get { return _navigateToCourseModule; }
            set { this.RaiseAndSetIfChanged(ref _navigateToCourseModule, value); }
        }

        #endregion
    }
}