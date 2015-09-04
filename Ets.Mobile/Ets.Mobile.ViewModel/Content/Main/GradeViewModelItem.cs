using System;
using System.Reactive.Linq;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeViewModelItem : ReactiveObject
    {
        private CourseVm _course;
        public CourseVm Course {
            get { return _course; }
            set { this.RaiseAndSetIfChanged(ref _course, value); }
        }

        private string _semester;
        public string Semester {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        public GradeViewModelItem(string semester, CourseVm course)
        {
            Semester = semester;
            Course = course;
        }
    }
}
