using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Content.Moodle.Courses.Content;
using Ets.Mobile.ViewModel.Contracts.Moodle.Courses;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.System;

namespace Ets.Mobile.ViewModel.Pages.Moodle.Courses
{
    [DataContract]
    public class MoodleCourseModuleContentPageViewModel : ViewModelBase, IMoodleCourseModuleContentPageViewModel
    {
        public MoodleCourseModuleContentPageViewModel(IScreen screen, MoodleCourseVm course, MoodleCourseContentVm content, MoodleCourseModuleVm courseModule) : base(screen, "MoodleCourseModuleContent")
        {
            Course = course;
            CourseContent = content;
            CourseModule = courseModule;
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            _navigateToModuleContentItem = ReactiveCommand.CreateAsyncObservable(NavigateToModuleContentItemImpl);
            _navigateToModuleContentItem.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });
            _navigateToModuleContentItem.Subscribe(async url => await Launcher.LaunchUriAsync(new Uri(url)));

            CoursesModuleContentsItems = new ReactiveList<MoodleCourseModuleContentSummaryViewModel>(
                CourseModule.Contents.Any() ? CourseModule.Contents.Select(c => new MoodleCourseModuleContentSummaryViewModel(Course, CourseContent, CourseModule, c, _navigateToModuleContentItem)) : new List<MoodleCourseModuleContentSummaryViewModel>()
            );

            CoursesModuleContents = CoursesModuleContentsItems.CreateDerivedCollection(x => x, x => x.Dispose());

            LoadCoursesModuleContent = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchCourseModuleContentsImpl());

            LoadCoursesModuleContent.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            CoursesModuleContentPresenter = LoadCoursesModuleContent.CreateReactivePresenter(CoursesModuleContentsItems, CoursesModuleContents, true);

            if (!CoursesModuleContentsItems.Any())
            {
                CoursesModuleContentPresenter.OnNextEmptyMessage();
            }
        }

        private IObservable<string> NavigateToModuleContentItemImpl(object param)
        {
            var selectedItem = param as MoodleCourseModuleContentSummaryViewModel;
            if (!string.IsNullOrEmpty(selectedItem?.CourseModuleContent.FileUrl))
            {
                return ClientServices().MoodleService.GetToken()
                    .ToObservable()
                    .Where(tkn => !string.IsNullOrEmpty(tkn))
                    .SelectMany(token =>
                    {
                        var url = selectedItem.CourseModuleContent.FileUrl + "&token=" + token;
                        return Observable.Return(url);
                    });
            }
            return Observable.Empty<string>();
        }

        private IObservable<List<MoodleCourseModuleContentSummaryViewModel>> FetchCourseModuleContentsImpl()
        {
            var fetchCoursesContents = Cache.GetAndFetchLatest(ViewModelKeys.MoodleCoursesContentForCourse(Course.Id), FetchMoodleCoursesContents)
                    .Where(s => s.Any(sc => sc.Id == CourseContent.Id && sc.Modules.Any(m => m.Id == CourseModule.Id) && sc.Modules.First(m => m.Id == CourseModule.Id).Contents.Length > 0))
                    .Select(sc => sc.First(s => s.Id == CourseContent.Id).Modules.First(m => m.Id == CourseModule.Id).Contents.Select(courseModuleContent => new MoodleCourseModuleContentSummaryViewModel(Course, CourseContent, CourseModule, courseModuleContent, _navigateToModuleContentItem)).ToList())
                    .Publish();

            fetchCoursesContents.Connect();

            return fetchCoursesContents.ThrowIfEmpty();
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
        
        private ReactiveCommand<string> _navigateToModuleContentItem;
        public IReactiveDerivedList<MoodleCourseModuleContentSummaryViewModel> CoursesModuleContents { get; protected set; }
        public ReactivePresenterCommand<List<MoodleCourseModuleContentSummaryViewModel>> LoadCoursesModuleContent { get; set; }
        public IReactivePresenterHandler<IReactiveDerivedList<MoodleCourseModuleContentSummaryViewModel>> CoursesModuleContentPresenter { get; set; }
        public ReactiveList<MoodleCourseModuleContentSummaryViewModel> CoursesModuleContentsItems { get; set; }

        #endregion
    }
}