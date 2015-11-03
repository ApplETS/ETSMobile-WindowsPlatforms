using System;
using System.Collections.Generic;
using Ets.Mobile.Entities.Signets;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Contracts.Main;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Pages.Shared;
using Ets.Mobile.ViewModel.Pages.UserDetails;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.ViewModel;
using Themes;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class MainViewModelDt : DesignTimeBase, IMainViewModel
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
                        A = AppColors.Green.A,
                        R = AppColors.Green.R,
                        G = AppColors.Green.G,
                        B = AppColors.Green.B,
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
                        A = AppColors.Orange.A,
                        R = AppColors.Orange.R,
                        G = AppColors.Orange.G,
                        B = AppColors.Orange.B,
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
                            A = AppColors.Orange.A,
                            R = AppColors.Orange.R,
                            G = AppColors.Orange.G,
                            B = AppColors.Orange.B,
                        }
                    }, null)
                });

                TodayPresenter = ReactivePresenterViewModel<ReactiveList<ScheduleVm>>.Create(Observable.Return(todayItems));
                GradesPresenter = ReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>>.Create(Observable.Return(gradeItems));
                LoadCoursesForToday = ReactiveCommand.CreateAsyncTask(_ => Task.FromResult(new ScheduleVm[0]));
                LoadGrades = ReactiveCommand.CreateAsyncTask(_ => Task.FromResult(new List<GradeSummaryViewModelGroup>()));
                NavigateToSchedule = ReactiveCommand.CreateAsyncTask(_ => Task.FromResult(Unit.Default));
                SideNavigation = new SideNavigationViewModel(null, new UserDetailsViewModel(null)
                {
                    Profile = new UserDetailsVm()
                    {
                        Username = "AK83510"
                    }
                });
            }
        }

        private IReactivePresenterViewModel<ReactiveList<ScheduleVm>> _todayPresenter;
        public IReactivePresenterViewModel<ReactiveList<ScheduleVm>> TodayPresenter
        {
            get { return _todayPresenter; }
            set
            {
                _todayPresenter = value;
                OnPropertyChanged();
            }
        }

        private IReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>> _gradePresenter;
        public IReactivePresenterViewModel<ReactiveList<GradeSummaryViewModelGroup>> GradesPresenter
        {
            get { return _gradePresenter; }
            set
            {
                _gradePresenter = value;
                OnPropertyChanged();
            }
        }

        public ISideNavigationViewModel SideNavigation { get; }

        public ReactiveCommand<ScheduleVm[]> LoadCoursesForToday { get; }
        public ReactiveCommand<List<GradeSummaryViewModelGroup>> LoadGrades { get; }
        public ReactiveCommand<Unit> NavigateToSchedule { get; }
        public ReactiveCommand<Unit> NavigateToProgram { get; }
    }
}
