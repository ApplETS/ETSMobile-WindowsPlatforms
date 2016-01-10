using Ets.Mobile.Entities.Moodle;
using ReactiveUI;
using ReactiveUI.Extensions;
using System.Reactive;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Content.Moodle.Courses.Content
{
    [DataContract]
    public class MoodleCourseModuleContentSummaryViewModel : ReactiveObject, IMergeableObject<MoodleCourseModuleContentSummaryViewModel>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseModuleContentSummaryViewModel x, MoodleCourseModuleContentSummaryViewModel y)
        {
            return x.Course.Id == y.Course.Id
                && x.CourseContent.Id == y.CourseContent.Id
                && x.CourseModule.Id == y.CourseModule.Id
                && x.CourseModuleContent.FileName == y.CourseModuleContent.FileName;
        }

        public int GetHashCode(MoodleCourseModuleContentSummaryViewModel obj)
        {
            return obj.Course.Id.GetHashCode() ^
                obj.CourseContent.Id.GetHashCode() ^
                obj.CourseModule.Id.GetHashCode() ^
                obj.CourseModuleContent.FileName.GetHashCode();
        }

        public void MergeWith(MoodleCourseModuleContentSummaryViewModel other)
        {
            CourseContent.MergeWith(other.CourseContent);
            Course.MergeWith(other.Course);
            CourseModuleContent.MergeWith(other.CourseModuleContent);
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            NavigateToCourseModuleContentUrl = null;
            CourseModule = null;
            CourseContent = null;
            Course = null;
            CourseModuleContent = null;
        }

        #endregion

        public MoodleCourseModuleContentSummaryViewModel(MoodleCourseVm course, MoodleCourseContentVm courseContent, MoodleCourseModuleVm courseModule, MoodleCourseModuleContentVm courseModuleContent, ReactiveCommand<Unit> navigateToCourseModuleContentUrl)
        {
            Course = course;
            CourseContent = courseContent;
            CourseModule = courseModule;
            CourseModuleContent = courseModuleContent;
            NavigateToCourseModuleContentUrl = navigateToCourseModuleContentUrl;
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

        private MoodleCourseModuleVm _courseModule;
        [DataMember]
        public MoodleCourseModuleVm CourseModule
        {
            get { return _courseModule; }
            set { this.RaiseAndSetIfChanged(ref _courseModule, value); }
        }

        private MoodleCourseModuleContentVm _courseModuleContent;
        [DataMember]
        public MoodleCourseModuleContentVm CourseModuleContent
        {
            get { return _courseModuleContent; }
            set { this.RaiseAndSetIfChanged(ref _courseModuleContent, value); }
        }

        private ReactiveCommand<Unit> _navigateToCourseModuleContentUrl;
        public ReactiveCommand<Unit> NavigateToCourseModuleContentUrl
        {
            get { return _navigateToCourseModuleContentUrl; }
            set { this.RaiseAndSetIfChanged(ref _navigateToCourseModuleContentUrl, value); }
        }

        #endregion
    }
}