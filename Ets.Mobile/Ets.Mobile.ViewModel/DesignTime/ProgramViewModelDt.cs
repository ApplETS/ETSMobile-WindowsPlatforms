using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Contracts.Program;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Core;
using ReactiveUI.Xaml.Controls.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class ProgramViewModelDt : IProgramPageViewModel, INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public ProgramViewModelDt()
        {
            ProgramPresenter = new ReactivePresenterHandlerDesignTime<IReactiveDerivedList<ProgramVm>>(
                Observable.Return((IReactiveDerivedList<ProgramVm>)new List<ProgramVm>(new[]
                {
                    new ProgramVm
                    {
                        Name = "Hello"
                    },
                    new ProgramVm
                    {
                        Name = "Hello"
                    },
                    new ProgramVm
                    {
                        Name = "Hello"
                    }
                }).AsEnumerable())
            );
            FetchPrograms = ReactivePresenterCommand.CreateAsyncTask(x => Task.FromResult(default(ProgramVm[])));
        }

        private IReactivePresenterHandler<IReactiveDerivedList<ProgramVm>> _programPresenter;
        public IReactivePresenterHandler<IReactiveDerivedList<ProgramVm>> ProgramPresenter
        {
            get { return _programPresenter; }
            set
            {
                _programPresenter = value;
                OnPropertyChanged();
            }
        }

        public ReactivePresenterCommand<ProgramVm[]> FetchPrograms { get; }

        public ReactiveList<ProgramVm> ProgramItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReactiveDerivedList<ProgramVm> Program
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}