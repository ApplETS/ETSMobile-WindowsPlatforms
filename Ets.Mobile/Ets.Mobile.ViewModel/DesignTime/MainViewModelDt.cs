using System;
using System.Collections.Generic;
using Ets.Mobile.Entities.Signets;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel;
using Ets.Mobile.ViewModel.Content.Main;
using Ets.Mobile.ViewModel.Contracts;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Presenter;
using ReactiveUI.Xaml.Controls.ViewModel;
using Themes;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class MainViewModelDt : IMainViewModel, INotifyPropertyChanged
    {
        public class TodayTileVm
        {
            public ActivityVm Model { get; set; }
            public bool IsTimeRemainingVisible { get; set; }
            public string TimeRemaining { get; set; }
        }

        public class GradeTileVm
        {
            public GradeSummaryViewModelGroup ModelGroup { get; set; }
        }

        public MainViewModelDt()
        {
            if (DesignMode.DesignModeEnabled)
            {
                var todayItems = new List<TodayTileVm>
                {
                    new TodayTileVm
                    {
                        Model = new ActivityVm
                        {
                            Group = "01",
                            Acronym = "LOG240",
                            Title = "Analyse et conception de logiciels",
                            IsPrincipalActivity = true,
                            Type = "C",
                            StartHour = DateTime.Now.TimeOfDay,
                            EndHour = DateTime.Now.AddMinutes(90).TimeOfDay,
                            Day = Convert.ToInt32("5"),
                            DayName = "Friday",
                            Location = "A-1300",
                            Name = "Activité de cours",
                            A = AppColors.Green.A,
                            R = AppColors.Green.R,
                            G = AppColors.Green.G,
                            B = AppColors.Green.B,
                        },
                        IsTimeRemainingVisible = true,
                        TimeRemaining = "39"
                    },
                    new TodayTileVm {Model = new ActivityVm
                    {
                        Group = "01",
                        Acronym = "LOG210",
                        Title = "Analyse et conception de logiciels",
                        IsPrincipalActivity = true,
                        Type = "C",
                        StartHour = DateTime.Now.AddMinutes(120).TimeOfDay,
                        EndHour = DateTime.Now.AddMinutes(120).AddMinutes(90).TimeOfDay,
                        Day = Convert.ToInt32("5"),
                        DayName = "Friday",
                        Location = "A-1300",
                        Name = "Activité de cours",
                        A = AppColors.Red.A,
                        R = AppColors.Red.R,
                        G = AppColors.Red.G,
                        B = AppColors.Red.B
                    }},
                    new TodayTileVm {Model = new ActivityVm
                    {
                        Group = "01",
                        Acronym = "LOG330",
                        Title = "Assurance de la qualité des logiciels",
                        IsPrincipalActivity = true,
                        Type = "C",
                        StartHour = DateTime.Now.AddMinutes(240).TimeOfDay,
                        EndHour = DateTime.Now.AddMinutes(240).AddMinutes(90).TimeOfDay,
                        Day = Convert.ToInt32("5"),
                        DayName = "Friday",
                        Location = "A-1300",
                        Name = "Activité de cours",
                        A = AppColors.Orange.A,
                        R = AppColors.Orange.R,
                        G = AppColors.Orange.G,
                        B = AppColors.Orange.B
                    }},
                };

                var gradeItems = new List<GradeTileVm>
                {
                    new GradeTileVm
                    {
                        ModelGroup = new GradeSummaryViewModelGroup("H2015", new []
                        {
                            new CourseVm
                            {
                                Group = "01",
                                Acronym = "LOG210",
                                Name = "Activité de cours",
                                A = AppColors.Red.A,
                                R = AppColors.Red.R,
                                G = AppColors.Red.G,
                                B = AppColors.Red.B
                            }
                        }, null)
                        {
                            Key = "H2015",
                            GradesItems = new ReactiveList<GradeSummaryViewModelItem>
                            {
                                new GradeSummaryViewModelItem("H2015", new CourseVm
                                {
                                    Group = "01",
                                    Acronym = "LOG210",
                                    Name = "Activité de cours",
                                    A = AppColors.Red.A,
                                    R = AppColors.Red.R,
                                    G = AppColors.Red.G,
                                    B = AppColors.Red.B,
                                }, null)
                            }
                        }
                    }
                };

                TodayPresenter = ReactivePresenterViewModel<List<TodayTileVm>>.Create(Observable.Return(todayItems));
                GradePresenter = ReactivePresenterViewModel<List<GradeTileVm>>.Create(Observable.Return(gradeItems));
            }
        }

        private IReactivePresenterViewModel<List<TodayTileVm>> _todayPresenter;

        public IReactivePresenterViewModel<List<TodayTileVm>> TodayPresenter
        {
            get { return _todayPresenter; }
            set
            {
                _todayPresenter = value;
                OnPropertyChanged();
            }
        }

        private IReactivePresenterViewModel<List<GradeTileVm>> _gradePresenter;

        public IReactivePresenterViewModel<List<GradeTileVm>> GradePresenter
        {
            get { return _gradePresenter; }
            set
            {
                _gradePresenter = value;
                OnPropertyChanged();
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
