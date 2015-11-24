using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Akavache;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Pages.Account;
using ReactiveUI;
using Splat;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;

namespace Ets.Mobile.ViewModel.Pages.Shared
{
    public class SideNavigationViewModel : ReactiveObject, ISideNavigationViewModel
    {
        public SideNavigationViewModel(IScreen screen)
        {
            Screen = screen ?? Locator.Current.GetService<IScreen>();
            UserDetails = Locator.Current.GetService<IUserDetailsViewModel>();
            OnViewModelCreation();
        }

        private void OnViewModelCreation()
        {
            IsSideNavigationVisibleSubject = new ReplaySubject<bool>();
            IsSideNavigationVisibleSubject.Subscribe(isSnVisible => IsSideNavigationVisible = isSnVisible);

            Logout = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                await BlobCache.UserAccount.InvalidateAll().ToTask();
                IsSideNavigationVisibleSubject.OnNext(false);
                Screen.Router.NavigateAndReset.Execute(new LoginViewModel(Locator.Current.GetService<IScreen>()));
                return Unit.Default;
            });
            
            OpenMenu = ReactiveCommand.CreateAsyncTask(_ =>
            {
                IsSideNavigationVisibleSubject.OnNext(true);
                IsSideNavigationVisible = true;
                return Task.FromResult(IsSideNavigationVisible);
            });

            CloseMenu = ReactiveCommand.CreateAsyncTask(_ =>
            {
                IsSideNavigationVisibleSubject.OnNext(false);
                IsSideNavigationVisible = false;
                return Task.FromResult(IsSideNavigationVisible);
            });

            Screen.Router.CurrentViewModel.Where(_ => _ != null).ObserveOn(RxApp.MainThreadScheduler).Subscribe(currentVm =>
            {
                // Current Page Set
                CurrentPage = Locator.Current.GetService<ResourceLoader>().GetString(currentVm.UrlPathSegment);
                CurrentViewModelType = currentVm.GetType();

                // Highlights the corresponding current ViewModel
                IsMain = MainTypes.Contains(CurrentViewModelType);
                IsSchedule = ScheduleTypes.Contains(CurrentViewModelType);
                IsGrade = GradeTypes.Contains(CurrentViewModelType);
                IsProgram = ProgramTypes.Contains(CurrentViewModelType);
            });
            
            UserDetails.LoadProfile
                .ObserveOn(RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
            {
                Profile = x;
            });
        }

        #region Properties

        private UserDetailsVm _profile;
        [DataMember]
        public UserDetailsVm Profile
        {
            get { return _profile; }
            set { this.RaiseAndSetIfChanged(ref _profile, value); }
        }

        private IScreen Screen { get; }

        public ReactiveCommand<Unit> Logout { get; set; }

        public ReactiveCommand<bool> OpenMenu { get; set; }

        public ReactiveCommand<bool> CloseMenu { get; set; }

        private ReactiveCommand<Type> _navCommand;

        public ReactiveCommand<Type> NavCommand
        {
            get
            {
                return _navCommand ?? (_navCommand = ReactiveCommand.CreateAsyncTask(_ =>
                {
                    var navType = _ as Type;
                    if (navType != CurrentViewModelType)
                    {
                        IsSideNavigationVisible = false;
                        Screen.Router.Navigate.Execute(Activator.CreateInstance(navType, Screen));
                    }
                    return Task.FromResult(navType);
                }));
            }
        }

        private IUserDetailsViewModel _userDetails;
        public IUserDetailsViewModel UserDetails
        {
            get { return _userDetails; }
            set { this.RaiseAndSetIfChanged(ref _userDetails, value); }
        }

        private string _currentPage;
        public string CurrentPage
        {
            get { return _currentPage; }
            set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
        }

        private Type _currentViewModelType;
        public Type CurrentViewModelType
        {
            get { return _currentViewModelType; }
            set { this.RaiseAndSetIfChanged(ref _currentViewModelType, value); }
        }

        private bool _isSideNavigationVisible;
        public bool IsSideNavigationVisible
        {
            get { return _isSideNavigationVisible; }
            set { this.RaiseAndSetIfChanged(ref _isSideNavigationVisible, value); }
        }

        public ISubject<bool> IsSideNavigationVisibleSubject { get; set; } 

        #region Menu Items

        private bool _isMain;
        public bool IsMain
        {
            get { return _isMain; }
            set { this.RaiseAndSetIfChanged(ref _isMain, value); }
        }

        public Type[] MainTypes { get; } = { typeof (MainViewModel) };

        private bool _isSchedule;
        public bool IsSchedule
        {
            get { return _isSchedule; }
            set { this.RaiseAndSetIfChanged(ref _isSchedule, value); }
        }

        public Type[] ScheduleTypes { get; } = { typeof(ScheduleViewModel) };

        private bool _isGrade;
        public bool IsGrade
        {
            get { return _isGrade; }
            set { this.RaiseAndSetIfChanged(ref _isGrade, value); }
        }

        public Type[] GradeTypes { get; } = { typeof(SelectCourseForGradeViewModel), typeof(GradeViewModel) };

        private bool _isProgram;
        public bool IsProgram
        {
            get { return _isProgram; }
            set { this.RaiseAndSetIfChanged(ref _isProgram, value); }
        }

        public Type[] ProgramTypes { get; } = { typeof(ProgramViewModel) };

        private bool _isSettings;
        public bool IsSettings
        {
            get { return _isSettings; }
            set { this.RaiseAndSetIfChanged(ref _isSettings, value); }
        }

        public Type[] SettingsTypes { get; } = { typeof(SettingsViewModel) };

        #endregion

        #endregion
    }
}