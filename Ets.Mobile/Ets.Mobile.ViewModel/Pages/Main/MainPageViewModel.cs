using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Main;
using ReactiveUI;
using System;
using System.Runtime.Serialization;

namespace Ets.Mobile.ViewModel.Pages.Main
{
    [DataContract]
    public partial class MainPageViewModel : ViewModelBase, IMainPageViewModel, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
        
        public MainPageViewModel(IScreen screen)
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