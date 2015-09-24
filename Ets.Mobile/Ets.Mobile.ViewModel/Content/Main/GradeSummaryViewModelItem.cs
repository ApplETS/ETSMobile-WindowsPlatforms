using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.ViewModel.Content.Main
{
    public class GradeSummaryViewModelItem : ReactiveObject
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

        public ReactiveCommand<Unit> NavigateToGrade { get; protected set; }

        public GradeSummaryViewModelItem(string semester, CourseVm course, ReactiveCommand<Unit> command)
        {
            Semester = semester;
            Course = course;
            NavigateToGrade = command;
        }
    }
}
