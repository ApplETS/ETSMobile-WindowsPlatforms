using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Contracts.Main;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Pages.Shared;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class MainViewModelDt : DesignTimeBase, IMainPageViewModel
    {
        public MainViewModelDt()
        {
            if (DesignMode.DesignModeEnabled)
            {
                var todayItems = new ReactiveList<ScheduleVm>(new List<ScheduleVm>
                {
                    new ScheduleVm
                    {
                        Title = "Analyse et conception de logiciels",
                        Location = "A-1300",
                        Name = "Activité de cours",
                        CourseAndGroup = "LOG430-01",
                        Description = "Description",
                        StartDate = DateTime.Now.AddMinutes(2),
                        EndDate = DateTime.Now.AddMinutes(62),
                        Color = "#ea7635"
                    },
                    new ScheduleVm
                    {
                        Title = "Analyse et conception de logiciels",
                        Location = "A-1300",
                        Name = "Activité de cours",
                        CourseAndGroup = "LOG430-01",
                        Description = "Description",
                        StartDate = DateTime.Now.AddMinutes(2),
                        EndDate = DateTime.Now.AddMinutes(62),
                        Color = "#ea7635"
                    }
                });

                var gradeItems = new ReactiveList<GradeSummaryViewModelGroup>(new List<GradeSummaryViewModelGroup>
                {
                    new GradeSummaryViewModelGroup("H2015", new [] {
                        new CourseVm
                        {
                            Acronym = "LOG430",
                            Grade = "A",
                            Name = "Architecture",
                            Semester = "H2015",
                            Program = "",
                            Color = "#ea7635"
                        }
                    }, null)
                });

                TodayPresenter = new ReactivePresenterHandlerDesignTime<IReactiveDerivedList<ScheduleVm>>(Observable.Return(new ReactiveDerivedListDesignTime<ScheduleVm>(todayItems)));
                GradesPresenter = new ReactivePresenterHandlerDesignTime<IReactiveDerivedList<GradeSummaryViewModelGroup>>(Observable.Return(new ReactiveDerivedListDesignTime<GradeSummaryViewModelGroup>(gradeItems)));
                LoadCoursesForToday = ReactivePresenterCommand.CreateAsyncTask(_ => Task.FromResult(new ScheduleVm[0]));
                LoadCoursesSummaries = ReactivePresenterCommand.CreateAsyncTask(_ => Task.FromResult(new List<GradeSummaryViewModelGroup>()));
                NavigateToSchedule = ReactiveCommand.CreateAsyncTask(_ => Task.FromResult(Unit.Default));
                NavigateToProgram = ReactiveCommand.CreateAsyncTask(_ => Task.FromResult(Unit.Default));
                SideNavigation = new SideNavigationViewModel(null)
                {
                    UserDetails = new UserDetailsViewModel(null)
                    {
                        Profile = new UserDetailsVm
                        {
                            Username = "AK83510"
                        }
                    }
                };

                IsAppBarVisible = true;
            }
        }

        private IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> _todayPresenter;
        public IReactivePresenterHandler<IReactiveDerivedList<ScheduleVm>> TodayPresenter
        {
            get { return _todayPresenter; }
            set
            {
                _todayPresenter = value;
                OnPropertyChanged();
            }
        }

        public bool IsAppBarVisible { get; set; }

        private IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> _gradePresenter;
        public IReactivePresenterHandler<IReactiveDerivedList<GradeSummaryViewModelGroup>> GradesPresenter
        {
            get { return _gradePresenter; }
            set
            {
                _gradePresenter = value;
                OnPropertyChanged();
            }
        }

        public ISideNavigationViewModel SideNavigation { get; }

        public ReactivePresenterCommand<ScheduleVm[]> LoadCoursesForToday { get; }
        public ReactivePresenterCommand<List<GradeSummaryViewModelGroup>> LoadCoursesSummaries { get; }
        public ReactiveCommand<Unit> NavigateToSchedule { get; }
        public ReactiveCommand<Unit> NavigateToProgram { get; }
    }
}