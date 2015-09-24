using System;
using System.Reactive;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Schedule;
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
            InitializeNavigations();
        }

        #region Navigate

        protected void InitializeNavigations()
        {
            NavigateToSchedule = ReactiveCommand.CreateAsyncTask(_ =>
            {
                HostScreen.Router.Navigate.Execute(new ScheduleViewModel(HostScreen));
                return Task.FromResult(Unit.Default);
            });
        }

        public ReactiveCommand<Unit> NavigateToSchedule { get; protected set; }
        
        public ReactiveCommand<Unit> NavigateToGrade { get; protected set; }

        #endregion
    }
}
