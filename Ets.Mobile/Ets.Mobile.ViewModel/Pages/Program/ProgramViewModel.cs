using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Contracts.Program;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Extensions;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Pages.Program
{
    public class ProgramViewModel : ViewModelBase, IProgramViewModel
    {
        public ProgramViewModel(IScreen screen) : base(screen, "Program")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            ProgramItems = new ReactiveList<ProgramVm>();
            LoadProgram = ReactivePresenterCommand.CreateAsyncObservable(_ => Cache.GetAndFetchLatest(ViewModelKeys.Program, () => ClientServices().SignetsService.Programs()).ThrowIfEmpty());

            LoadProgram.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });
            
            Program = ProgramItems.CreateDerivedCollection(
                x => x,
                x => x.Dispose(),
                orderer: (p1, p2) => _programComparer.Compare(p1, p2)
            );

            ProgramPresenter = LoadProgram.CreateReactivePresenter(ProgramItems, Program, true);
        }

        #region Properties

        private readonly IComparer<ProgramVm> _programComparer = new ProgramsComparator();
        [DataMember]
        public ReactiveList<ProgramVm> ProgramItems { get; protected set; }
        [DataMember]
        public IReactiveDerivedList<ProgramVm> Program { get; protected set; }
        public IReactivePresenterHandler<IReactiveDerivedList<ProgramVm>> ProgramPresenter { get; protected set; }
        public ReactivePresenterCommand<ProgramVm[]> LoadProgram { get; protected set; }

        #endregion
    }
}