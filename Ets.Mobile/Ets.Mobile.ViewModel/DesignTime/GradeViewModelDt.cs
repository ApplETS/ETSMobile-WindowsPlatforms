using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Grade;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Presenter;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class GradeViewModelDt : ReactiveObject
    {
        public GradeViewModelDt()
        {
            Semester = "É2015";
            OnViewModelCreation();
        }

        protected void OnViewModelCreation()
        {
            GradeItems = new ReactiveList<GradeViewModelItemDt>
            {
                new GradeViewModelItemDt
                {
                    Course = new CourseVm
                    {
                        Semester = "É2015",
                        A = Colors.Orange.A,
                        R = Colors.Orange.R,
                        G = Colors.Orange.G,
                        B = Colors.Orange.B,
                        Brush = {Color = Colors.Orange, Opacity = 1}
                    },
                    Evaluation = new EvaluationsVm
                    {
                        ActualGrade = 78.4,
                        A = Colors.Orange.A,
                        R = Colors.Orange.R,
                        G = Colors.Orange.G,
                        B = Colors.Orange.B,
                        Brush = {Color = Colors.Orange, Opacity = 1}
                    },
                    GradesPresenter = new ReactivePresenterViewModel<EvaluationsVm>
                    {
                        Content = Observable.Return(new EvaluationsVm
                        {
                            ActualGrade = 78.4,
                            A = Colors.Orange.A,
                            R = Colors.Orange.R,
                            G = Colors.Orange.G,
                            B = Colors.Orange.B,
                            Brush = {Color = Colors.Orange, Opacity = 1}
                        })
                    }
                }
            };

            Grades = GradeItems.CreateDerivedCollection(
                x => new GradeTileViewModelDt(x)
            );
        }

        public string Semester { get; set; }

        public ReactiveList<GradeViewModelItemDt> GradeItems { get; set; }

        public IReactiveDerivedList<GradeTileViewModelDt> Grades { get; protected set; }

        public class GradeTileViewModelDt
        {
            public GradeViewModelItemDt Model { get; protected set; }

            public GradeTileViewModelDt(GradeViewModelItemDt model)
            {
                Model = model;
            }
        }
    }

    public class GradeViewModelItemDt : ReactiveObject
    {
        private CourseVm _course;
        public CourseVm Course { get { return _course; } set { this.RaiseAndSetIfChanged(ref _course, value); } }

        public EvaluationsVm Evaluation { get; set; }

        public IReactivePresenterViewModel<EvaluationsVm> GradesPresenter { get; set; }
    }

    public class GradeItemViewModelDt : ReactiveObject
    {
        public GradeItemViewModelDt()
        {
            Title = "Résultat";
            Grade = "90";
            BackgroundBrush = new SolidColorBrush(Colors.Orange);
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        private string _grade;
        public string Grade
        {
            get { return _grade; }
            set { this.RaiseAndSetIfChanged(ref _grade, value); }
        }

        private Brush _brush;
        public Brush BackgroundBrush
        {
            get { return _brush; }
            set { this.RaiseAndSetIfChanged(ref _brush, value); }
        }
    }
}