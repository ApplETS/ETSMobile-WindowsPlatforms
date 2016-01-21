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
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Pages.Program
{
    public class ProgramPageViewModel : ViewModelBase, IProgramPageViewModel
    {
        public ProgramPageViewModel(IScreen screen) : base(screen, "Program")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            ProgramItems = new ReactiveList<ProgramVm>();
            FetchPrograms = ReactivePresenterCommand.CreateAsyncObservable(_ => FetchProgramsImpl());

            FetchPrograms.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                });
            
            Program = ProgramItems.CreateDerivedCollection(
                x => x,
                x => x.Dispose(),
                orderer: (p1, p2) => _programComparer.Compare(p1, p2)
            );

            ProgramPresenter = FetchPrograms.CreateReactivePresenter(ProgramItems, Program, true);
        }

        private IObservable<ProgramVm[]> FetchProgramsImpl()
        {
            var fetchPrograms =
                Cache.GetAndFetchLatest(ViewModelKeys.Program, () => ClientServices().SignetsService.Programs())
                    .Publish();

            fetchPrograms.Connect();

            return fetchPrograms.ThrowIfEmpty();
        }

        #region Properties

        private readonly IComparer<ProgramVm> _programComparer = new ProgramsComparator();
        [DataMember]
        public ReactiveList<ProgramVm> ProgramItems { get; protected set; }
        [DataMember]
        public IReactiveDerivedList<ProgramVm> Program { get; protected set; }
        public IReactivePresenterHandler<IReactiveDerivedList<ProgramVm>> ProgramPresenter { get; protected set; }
        public ReactivePresenterCommand<ProgramVm[]> FetchPrograms { get; protected set; }

        #endregion
    }
}