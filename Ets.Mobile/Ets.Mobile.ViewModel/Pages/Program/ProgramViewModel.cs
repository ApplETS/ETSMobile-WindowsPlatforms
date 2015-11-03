using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Comparators;
using Ets.Mobile.ViewModel.Content.Program;
using Ets.Mobile.ViewModel.Contracts.Program;
using Messaging.UniversalApp.Common;
using ReactiveUI;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.ViewModel;
using Refit;
using Splat;

namespace Ets.Mobile.ViewModel.Pages.Program
{
    public class ProgramViewModel : PageViewModelBase, IProgramViewModel
    {
        public ProgramViewModel(IScreen screen) : base(screen, "Program")
        {
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            ProgramItems = new ReactiveList<ProgramVm>();
            LoadProgram = ReactiveDeferedCommand.CreateAsyncObservable(() =>
            {
                return Cache.GetAndFetchLatest(ViewModelKeys.Program, () => ClientServices().SignetsService.Programs());
            });

            LoadProgram.ThrownExceptions
                .Subscribe(x =>
                {
                    UserError.Throw(x.Message, x);
                    Exception exception;
                    var apiException = x as ApiException;
                    if (apiException != null)
                    {
                        var exceptionMessage = new ErrorMessageContent(x.Message, apiException);
                        if (apiException.ReasonPhrase == "Not Found")
                        {
                            exceptionMessage.Message = Resources().GetString("NetworkError");
                            exceptionMessage.Title = Resources().GetString("NetworkTitleError");
                        }
                        exception = exceptionMessage.Exception;
                    }
                    else
                    {
                        exception = x;
                    }
                    _programExceptionSubject.OnNext(exception);
                });

            LoadProgram.Subscribe(x =>
            {
                ProgramItems.Clear();
                ProgramItems.AddRange(x);
            });

            Program = ProgramItems.CreateDerivedCollection(
                x => new ProgramTileViewModel(x),
                x => x.Dispose(),
                orderer: (x, y) => _programComparer.Compare(x.Model, y.Model)
            );

            ProgramPresenter = ReactivePresenterViewModel<ReactiveList<ProgramVm>>.Create(ProgramItems, Program, LoadProgram.IsExecuting, _programExceptionSubject);
        }

        #region Properties

        private readonly IComparer<ProgramVm> _programComparer = new ProgramsComparator();
        [DataMember]
        public ReactiveList<ProgramVm> ProgramItems { get; protected set; }
        [DataMember]
        public IReactiveDerivedList<ProgramTileViewModel> Program { get; protected set; }
        public IReactivePresenterViewModel<ReactiveList<ProgramVm>> ProgramPresenter { get; protected set; }
        public ReactiveCommand<ProgramVm[]> LoadProgram { get; protected set; }
        private readonly ReplaySubject<Exception> _programExceptionSubject = new ReplaySubject<Exception>();

        #endregion
    }
}
