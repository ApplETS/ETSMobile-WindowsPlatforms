using System;
using System.Runtime.Serialization;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts;
using ReactiveUI;

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
            : base(screen, "Main")
        {
            OnViewModelCreation();
        }

        protected override sealed void OnViewModelCreation()
        {
            InitializeSideNavigation();
            InitializeToday();
            InitializeGrade();
        }
    }
}
