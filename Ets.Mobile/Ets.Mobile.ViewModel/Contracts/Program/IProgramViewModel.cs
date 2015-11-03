using System;
using System.Collections.Generic;
using System.Text;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.ViewModel;

namespace Ets.Mobile.ViewModel.Contracts.Program
{
    public interface IProgramViewModel
    {
        IReactivePresenterViewModel<ReactiveList<ProgramVm>> ProgramPresenter { get; }
        ReactiveCommand<ProgramVm[]> LoadProgram { get; }
    }
}
