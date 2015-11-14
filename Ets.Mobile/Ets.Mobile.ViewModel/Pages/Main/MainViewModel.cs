using System;
using System.Reactive;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts;
using Ets.Mobile.ViewModel.Contracts.Main;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Schedule;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    [DataContract]
    public partial class MainViewModel : PageViewModelBase, IMainViewModel, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
        
        public MainViewModel(IScreen screen)
            : base(screen, "Home")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            InitializeToday();
            InitializeGrade();
        }
    }
}
