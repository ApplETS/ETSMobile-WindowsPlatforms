using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Contracts.Program;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.ViewModel;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class ProgramViewModelDt : IProgramViewModel, INotifyPropertyChanged
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
            ProgramPresenter = new ReactivePresenterViewModel<ReactiveList<ProgramVm>>
            {
                Content = Observable.Return(new ReactiveList<ProgramVm>(new[]
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
                }))
            };
            LoadProgram = ReactiveCommand.CreateAsyncTask(x => Task.FromResult(default(ProgramVm[])));
        }

        private IReactivePresenterViewModel<ReactiveList<ProgramVm>> _programPresenter;
        public IReactivePresenterViewModel<ReactiveList<ProgramVm>> ProgramPresenter
        {
            get { return _programPresenter; }
            set
            {
                _programPresenter = value;
                OnPropertyChanged();
            }
        }

        public ReactiveCommand<ProgramVm[]> LoadProgram { get; }
    }
}
