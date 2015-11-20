using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Contracts.Program;
using Ets.Mobile.ViewModel.Mixins;
using Messaging.Interfaces.Popup;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using Splat;

namespace Ets.Mobile.ViewModel.Pages.Program
{
    public class ProgramViewModel : PageViewModelBase, IProgramViewModel
    {
        public ProgramViewModel(IScreen screen) : base(screen, "Program")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            ProgramItems = new ReactiveList<ProgramVm>();
            LoadProgram = ReactivePresenterCommand.CreateAsyncObservable(_ =>
            {
                return Cache.GetAndFetchLatest(ViewModelKeys.Program, () => ClientServices().SignetsService.Programs());
            });

            LoadProgram.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                    x.HandleOfflineConnection(ViewServices().Notification);
                });

            LoadProgram.Subscribe(x =>
            {
                ProgramItems.Clear();
                ProgramItems.AddRange(x);
            });

            Program = ProgramItems.CreateDerivedCollection(
                x => x,
                x => x.Dispose(),
                orderer: (x, y) => _programComparer.Compare(x, y)
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