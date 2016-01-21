using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Content.Grade;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Contracts.Grade
{
    public interface IGradePageViewModel
    {
        ReactiveCommand<GradeViewModelItem[]> LoadGrade { get; }
        string Semester { get; set; }
        CourseVm SelectedCourse { get; set; }
        ReactiveList<GradeViewModelItem> GradeItems { get; set; }
    }
}