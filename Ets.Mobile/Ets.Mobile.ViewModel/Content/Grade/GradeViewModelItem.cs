using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Runtime.Serialization;

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
        
        public GradeViewModelItem(CourseVm course)
        {
            Course = course;

            HasTriggeredLoadGradeOnce = false;

            OnViewModelCreation();
        }

        private void OnViewModelCreation()
        {
            LoadGrade = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchGradesImpl());

            LoadGrade.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            GradesPresenter = LoadGrade.CreateReactivePresenter();
        }

        private IObservable<EvaluationsVm> FetchGradesImpl()
        {
            return Cache.GetAndFetchLatest(ViewModelKeys.GradesForSemesterAndCourse(Course.Semester, Course.Name),
                () => ClientServices()
                        .SignetsService.Evaluations(Course.Acronym, Course.Group, Course.Semester)
                        .ApplyCustomColors(SettingsService(), Course)
            );
        } 

        #region Properties

        private CourseVm _course;
        [DataMember]
        public CourseVm Course
        {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        public IReactivePresenterHandler<EvaluationsVm> GradesPresenter { get; protected set; }
        public ReactivePresenterCommand<EvaluationsVm> LoadGrade { get; private set; }
        public bool HasTriggeredLoadGradeOnce { get; set; }

        #endregion
    }
}