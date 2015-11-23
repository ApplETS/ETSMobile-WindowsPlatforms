using System;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using ReactiveUI;
using System.Reactive.Threading.Tasks;
using Windows.UI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;

namespace Ets.Mobile.ViewModel.Content.Grade
{
    [DataContract]
    public class GradeViewModelItem : ApplicationServicesBase, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
        }

        #endregion

        private CourseVm _course;
        [DataMember] public CourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private EvaluationsVm _evaluation;
        [DataMember] public EvaluationsVm Evaluation
        {
            get { return _evaluation; }
            set { this.RaiseAndSetIfChanged(ref _evaluation, value); }
        }
        
        public IReactivePresenterHandler<EvaluationsVm> GradesPresenter { get; protected set; }
        public ReactivePresenterCommand<EvaluationsVm> LoadGrade { get; protected set; }
        public bool HasTriggeredLoadGradeOnce { get; set; }
        
        public GradeViewModelItem(CourseVm course)
        {
            Course = course;

            HasTriggeredLoadGradeOnce = false;

            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            LoadGrade = ReactivePresenterCommand.CreateAsyncObservable(_ =>
            {
                return Cache.GetAndFetchLatest(ViewModelKeys.GradesForSemesterAndCourse(Course.Semester, Course.Name),
                    () => ClientServices().SignetsService.Evaluations(Course.Acronym, Course.Group, Course.Semester)
                        .ToObservable()
                        .Do(async grade => {
                            grade.LetterGrade = Course.Grade;
                            await SettingsService().ApplyColorOnItemsForSemester(grade, Course.Semester, x => Course.Acronym, Color.FromArgb(Course.A, Course.R, Course.G, Course.B));
                        })
                );
            });
            
            LoadGrade.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            LoadGrade.Subscribe(e => Evaluation = e);

            GradesPresenter = LoadGrade.CreateReactivePresenter();
        }
    }
}