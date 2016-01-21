using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;

namespace Ets.Mobile.ViewModel.Contracts.Program
{
    public interface IProgramPageViewModel
    {
        ReactiveList<ProgramVm> ProgramItems { get; }
        IReactiveDerivedList<ProgramVm> Program { get; }
        ReactivePresenterCommand<ProgramVm[]> FetchPrograms { get; }
    }
}