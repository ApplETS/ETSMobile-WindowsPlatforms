using Akavache;
using Ets.Mobile.Client.Mixins;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Grade;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.Pages.Grade
{
    [DataContract]
    public class GradePageViewModel : ViewModelBase, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            Semester = null;
            GradeItems.Clear();
        }

        #endregion
        
        public GradePageViewModel(IScreen screen, CourseVm selectedCourse) 
            : base(screen, "Grades")
        {
            Semester = selectedCourse.Semester;
            SelectedCourse = selectedCourse;
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            GradeItems = new ReactiveList<GradeViewModelItem>();
            
            LoadGrade = ReactiveCommand.CreateAsyncObservable(_ => FetchGradeItems());

            LoadGrade.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            LoadGrade.Subscribe(gradeItems =>
            {
                using (GradeItems.SuppressChangeNotifications())
                {
                    GradeItems.Clear();
                    GradeItems.AddRange(gradeItems.OrderBy(x => x.Course.Acronym != SelectedCourse.Acronym));
                }
            });
        }

        private IObservable<GradeViewModelItem[]> FetchGradeItems()
        {
            var fetchGradeItems = Cache.GetAndFetchLatest(ViewModelKeys.Courses, FetchCourses)
                    .Select(courses => courses.OrderByDescending(x => x.Semester, new SemestersComparator()).ToList())
                    .Select(x => x.Where(y => y.Semester == Semester))
                    .Select(gradeItems => gradeItems.Select(g => new GradeViewModelItem(g)).ToArray())
                    .Publish();

            fetchGradeItems.Connect();

            return fetchGradeItems;
        } 

        private Task<CourseVm[]> FetchCourses()
        {
            return ClientServices().SignetsService.Courses().ApplyCustomColors(SettingsService());
        }

        #region Properties

        public ReactiveCommand<GradeViewModelItem[]> LoadGrade { get; protected set; }
        
        private string _semester;
        [DataMember]
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private CourseVm _selectedCourse;
        [DataMember]
        public CourseVm SelectedCourse
        {
            get { return _selectedCourse; }
            set { this.RaiseAndSetIfChanged(ref _selectedCourse, value); }
        }

        private ReactiveList<GradeViewModelItem> _gradeItems;
        [DataMember]
        public ReactiveList<GradeViewModelItem> GradeItems
        {
            get { return _gradeItems; }
            set { this.RaiseAndSetIfChanged(ref _gradeItems, value); }
        }

        #endregion
    }    
}