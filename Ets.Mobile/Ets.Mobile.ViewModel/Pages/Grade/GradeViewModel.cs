﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Grade;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Pages.Grade
{
    [DataContract]
    public class GradeViewModel : ViewModelBase, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            Semester = null;
            GradeItems.Clear();
        }

        #endregion

        public GradeViewModel(IScreen screen, CourseVm selectedCourse) 
            : base(screen, "Grades")
        {
            Semester = selectedCourse.Semester;
            SelectedCourse = selectedCourse;
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            GradeItems = new ReactiveList<GradeViewModelItem>();
            
            LoadGrade = ReactiveCommand.CreateAsyncObservable(_ =>
            {
                return BlobCache.UserAccount.GetAndFetchLatest(ViewModelKeys.Courses, FetchCourses)
                    .Select(courses =>
                        courses.Where(x => x.Semester != "s.o.")
                            .OrderByDescending(x => x.Semester, new SemestersComparator())
                            .ToList()
                    )
                    .Where(x => x != null 
                        && x.Any(y => !string.IsNullOrEmpty(y.Semester)) 
                        && x.Any(y => y.Semester == Semester))
                    .Select(x => x.Where(y => y.Semester == Semester))
                    .Select(x => x.Select(y => new GradeViewModelItem(y)));
            });

            LoadGrade.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });

            LoadGrade.Subscribe(x =>
            {
                GradeItems.Clear();
                GradeItems.AddRange(x);
                InitialIndex = GradeItems.IndexOf(GradeItems.First(y => y.Course.Acronym == SelectedCourse.Acronym));
            });

            Grades = GradeItems.CreateDerivedCollection(
                x => x,
                x => x.Dispose()
            );
        }

        public IObservable<List<CourseVm>> FetchCourses()
        {
            return ClientServices().SignetsService.Courses()
                .ToObservable()
                .Do(async courses =>
                {
                    foreach (var course in courses.Where(x => x.Semester != "s.o.")
                        .GroupBy(x => x.Semester))
                    {
                        await SettingsService().ApplyColorOnItemsForSemester(
                            courses.Where(x => x.Semester == course.FirstOrDefault().Semester).ToArray(),
                            course.FirstOrDefault().Semester, x => x.Acronym);
                    }
                })
                .Select(x => x.ToList());
        }

        #region Properties

        public ReactiveCommand<IEnumerable<GradeViewModelItem>> LoadGrade { get; protected set; }
        
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

        private int _initialIndex;
        [DataMember]
        public int InitialIndex
        {
            get { return _initialIndex; }
            set { this.RaiseAndSetIfChanged(ref _initialIndex, value); }
        }

        private ReactiveList<GradeViewModelItem> _gradeItems;
        [DataMember]
        public ReactiveList<GradeViewModelItem> GradeItems
        {
            get { return _gradeItems; }
            set { this.RaiseAndSetIfChanged(ref _gradeItems, value); }
        }

        [DataMember]
        public IReactiveDerivedList<GradeViewModelItem> Grades { get; protected set; }

        #endregion
    }    
}