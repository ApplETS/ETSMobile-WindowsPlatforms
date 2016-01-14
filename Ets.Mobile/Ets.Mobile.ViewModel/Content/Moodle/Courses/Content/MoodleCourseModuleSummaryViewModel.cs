using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Pages.Moodle.Courses;
using ReactiveUI;
using ReactiveUI.Extensions;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Content.Moodle.Courses.Content
{
    [DataContract]
    public class MoodleCourseModuleSummaryViewModel : ReactiveObject, IMergeableObject<MoodleCourseModuleSummaryViewModel>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseModuleSummaryViewModel x, MoodleCourseModuleSummaryViewModel y)
        {
            return x.CourseModule.Id == y.CourseModule.Id;
        }

        public int GetHashCode(MoodleCourseModuleSummaryViewModel obj)
        {
            return obj.CourseModule.Id.GetHashCode();
        }

        public void MergeWith(MoodleCourseModuleSummaryViewModel other)
        {
            CourseContent.MergeWith(other.CourseContent);
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            NavigateToCourseModule = null;
            CourseModule = null;
            CourseContent = null;
            Course = null;
        }

        #endregion

        public MoodleCourseModuleSummaryViewModel(MoodleCourseVm course, MoodleCourseContentVm courseContent, MoodleCourseModuleVm courseModule, ReactiveCommand<MoodleCourseModuleContentPageViewModel> navigateToCourseModule)
        {
            Course = course;
            CourseContent = courseContent;
            CourseModule = courseModule;
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

        private MoodleCourseModuleVm _courseModule;
        [DataMember]
        public MoodleCourseModuleVm CourseModule
        {
            get { return _courseModule; }
            set { this.RaiseAndSetIfChanged(ref _courseModule, value); }
        }

        private ReactiveCommand<MoodleCourseModuleContentPageViewModel> _navigateToCourseModule;
        public ReactiveCommand<MoodleCourseModuleContentPageViewModel> NavigateToCourseModule
        {
            get { return _navigateToCourseModule; }
            set { this.RaiseAndSetIfChanged(ref _navigateToCourseModule, value); }
        }

        #endregion
    }
}