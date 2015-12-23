using System;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using ReactiveUI;
using System.Reactive.Threading.Tasks;
using Windows.UI;
using Ets.Mobile.Client.Mixins;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using Refit;

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
        
        public IReactivePresenterHandler<EvaluationsVm> GradesPresenter { get; protected set; }
        public ReactivePresenterCommand<EvaluationsVm> LoadGrade { get; private set; }
        public bool HasTriggeredLoadGradeOnce { get; set; }
        
        public GradeViewModelItem(CourseVm course)
        {
            Course = course;

            HasTriggeredLoadGradeOnce = false;

            OnViewModelCreation();
        }

        private void OnViewModelCreation()
        {
            LoadGrade = ReactivePresenterCommand.CreateAsyncObservable(_ =>
                Cache.GetAndFetchLatest(
                    ViewModelKeys.GradesForSemesterAndCourse(Course.Semester, Course.Name),
                    () =>
                        ClientServices()
                            .SignetsService.Evaluations(Course.Acronym, Course.Group, Course.Semester)
                            .ToObservable()
                            .ApplyCustomColors(SettingsService(), Course)
                )
            );

            GradesPresenter = LoadGrade.CreateReactivePresenter();
        }
    }
}