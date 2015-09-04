﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeViewModelGroup : ReactiveObject, IGrouping<string, GradeViewModelItem>, IDisposable
    {
        #region IGrouping<string, GradesViewModelItem>

        public string Key { get; set; }

        public IEnumerator<GradeViewModelItem> GetEnumerator()
        {
            return _gradesItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _gradesItems.GetEnumerator();
        }

        #endregion

        private string _semester;
        public string Semester {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private ReactiveList<GradeViewModelItem> _gradesItems;
        public ReactiveList<GradeViewModelItem> GradesItems {
            get { return _gradesItems; }
            set { this.RaiseAndSetIfChanged(ref _gradesItems, value); }
        }

        public GradeViewModelGroup(string semester, IEnumerable<CourseVm> courses)
        {
            // Semester
            Semester = semester;

            // Key
            Key = semester;

            // Courses
            GradesItems = new ReactiveList<GradeViewModelItem>();
            GradesItems.AddRange(courses.Select(y => new GradeViewModelItem(semester, y)));
        }

        public GradeViewModelGroup(string semester, IObservable<List<CourseVm>> courses)
        {
            // Semester
            Semester = semester;
            
            // Key
            Key = semester;

            // Courses
            courses.ObserveOnDispatcher().Subscribe(x =>
            {
                GradesItems.Clear();
                GradesItems.AddRange(x.Select(y => new GradeViewModelItem(semester, y)));
            });
        }

        public void Dispose()
        {
            GradesItems?.Clear();
        }
    }
}
