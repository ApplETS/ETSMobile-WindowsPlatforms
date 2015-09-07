using System;
using System.Reactive;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts;
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
            NavigateToSchedule = ReactiveCommand.CreateAsyncTask(_ =>
            {
                HostScreen.Router.Navigate.Execute(new ScheduleViewModel(HostScreen));
                return Task.FromResult(Unit.Default);
            });
        }

        #region Navigate

        public ReactiveCommand<Unit> NavigateToSchedule { get; protected set; }

        #endregion
    }
}
