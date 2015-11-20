using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;

namespace Ets.Mobile.ViewModel.Contracts.Program
{
    public interface IProgramViewModel
    {
        IReactivePresenterHandler<IReactiveDerivedList<ProgramVm>> ProgramPresenter { get; }
        ReactivePresenterCommand<ProgramVm[]> LoadProgram { get; }
    }
}