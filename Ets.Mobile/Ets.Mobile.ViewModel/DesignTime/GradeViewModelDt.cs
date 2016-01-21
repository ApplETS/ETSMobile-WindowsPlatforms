using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Handlers;
using System.Reactive.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class GradeViewModelDt : DesignTimeBase
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
                        Color = Colors.Orange.ToString()
                    },
                    Evaluation = new EvaluationsVm
                    {
                        ActualGrade = 78.4,
                        Color = Colors.Orange.ToString()
                    },
                    GradesPresenter = new ReactivePresenterHandlerDesignTime<EvaluationsVm>(
                        Observable.Return(new EvaluationsVm
                        {
                            ActualGrade = 78.4,
                            Color = Colors.Orange.ToString()
                        })
                    )
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

        public IReactivePresenterHandler<EvaluationsVm> GradesPresenter { get; set; }
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