using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Moodle.Courses
{
    [DataContract]
    public class MoodleCourseContentPageViewModel : ViewModelBase, IMoodleCourseContentPageViewModel
    {
        public MoodleCourseContentPageViewModel(IScreen screen, MoodleCourseVm course) : base(screen, "MoodleCourseContent")
        {
            Course = course;
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            _navigateToCourseModuleItem = ReactiveCommand.CreateAsyncObservable(NavigateToCourseModuleItemImpl);
            _navigateToCourseModuleItem.Subscribe(selectedItem => HostScreen.Router.Navigate.Execute(new MoodleCourseModulePageViewModel(HostScreen, selectedItem.Course, selectedItem.CourseContent)));

            CoursesContentItems = new ReactiveList<MoodleCourseContentSummaryViewModel>();

            CoursesContent = CoursesContentItems.CreateDerivedCollection(x => x, x => x.Dispose());

            LoadCoursesContent = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchMoodleCoursesImpl());

            LoadCoursesContent.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            CoursesContentPresenter = LoadCoursesContent.CreateReactivePresenter(CoursesContentItems, CoursesContent, true);
        }

        private IObservable<MoodleCourseModulePageViewModel> NavigateToCourseModuleItemImpl(object param)
        {
            var selectedItem = param as MoodleCourseContentSummaryViewModel;
            if (selectedItem != null)
            {
                return Observable.Return(new MoodleCourseModulePageViewModel(HostScreen, selectedItem.Course, selectedItem.CourseContent));
            }
            return Observable.Empty<MoodleCourseModulePageViewModel>();
        }

        private IObservable<List<MoodleCourseContentSummaryViewModel>> FetchMoodleCoursesImpl()
        {
            var fetchCourses = Cache.GetAndFetchLatest(ViewModelKeys.MoodleCoursesContentForCourse(Course.Id), FetchMoodleCoursesContents)
                    .Select(sc => sc.Select(courseContent => new MoodleCourseContentSummaryViewModel(Course, courseContent, _navigateToCourseModuleItem)).ToList())
                    .Publish();

            fetchCourses.Connect();
            
            return fetchCourses.ThrowIfEmpty();
        }

        private Task<MoodleCourseContentVm[]> FetchMoodleCoursesContents()
        {
            return ClientServices().MoodleService.CoursesContents(Course.Id).ApplyCustomColors(Course, SettingsService());
        }

        #region Properties

        private MoodleCourseVm _course;

        [DataMember]
        public MoodleCourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private ReactiveCommand<MoodleCourseModulePageViewModel> _navigateToCourseModuleItem;
        public IReactiveDerivedList<MoodleCourseContentSummaryViewModel> CoursesContent { get; protected set; }
        public ReactivePresenterCommand<List<MoodleCourseContentSummaryViewModel>> LoadCoursesContent { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<MoodleCourseContentSummaryViewModel>> CoursesContentPresenter { get; set; }
        public ReactiveList<MoodleCourseContentSummaryViewModel> CoursesContentItems { get; set; }

        #endregion
    }
}