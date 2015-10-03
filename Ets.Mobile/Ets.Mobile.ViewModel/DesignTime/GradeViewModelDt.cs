using System.Reactive.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Presenter;
using ReactiveUI.Xaml.Controls.ViewModel;

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
        public CourseVm Course { get; set; }

        public EvaluationsVm Evaluation { get; set; }

        public IReactivePresenterViewModel<EvaluationsVm> GradesPresenter { get; set; }
    }

    public class GradeItemViewModelDt
    {
        public GradeItemViewModelDt()
        {
            Title = "Résultat";
            Grade = "90";
            BackgroundBrush = new SolidColorBrush(Colors.Orange);
        }
        
        public string Title { get; set; }
        
        public string Grade { get; set; }
        
        public Brush BackgroundBrush { get; set; }
    }
}