using Akavache;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.ViewModel.Contracts.Shared;
using Ets.Mobile.ViewModel.Contracts.UserDetails;
using Ets.Mobile.ViewModel.Pages.Account;
using Ets.Mobile.ViewModel.Pages.Grade;
using Ets.Mobile.ViewModel.Pages.Main;
using Ets.Mobile.ViewModel.Pages.Moodle;
using Ets.Mobile.ViewModel.Pages.Program;
using Ets.Mobile.ViewModel.Pages.Schedule;
using Ets.Mobile.ViewModel.Pages.Settings;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
#if WINDOWS_PHONE_APP || WINDOWS_UWP
using Windows.UI.ViewManagement;
#endif

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

            Logout = ReactiveCommand.CreateAsyncTask(_ => LogoutImpl());
            
            OpenMenu = ReactiveCommand.CreateAsyncTask(_ => OpenMenuImpl());

            CloseMenu = ReactiveCommand.CreateAsyncTask(_ => CloseMenuImpl());

            Screen.Router
                .CurrentViewModel
                .Where(viewModel => viewModel != null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(SetCurrentViewModelImpl);
            
            UserDetails.LoadProfile
                .ObserveOn(RxApp.MainThreadScheduler)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(x => { Profile = x; });

            NavCommand = ReactiveCommand.CreateAsyncObservable(NavigateToViewModelImpl);

            NavCommand.Subscribe(viewModel =>
            {
                IsSideNavigationVisible = false;
                Screen.Router.Navigate.Execute(viewModel);
            });
        }

        private async Task<Unit> LogoutImpl()
        {
            await Locator.Current.GetService<ICalendarService>().RemoveScheduleFromCalendar();
            await Agent.ScheduleTileUpdaterBackgroundTask.Unregister();
            await BlobCache.UserAccount.InvalidateAll().ToTask();
            IsSideNavigationVisibleSubject.OnNext(false);
            Screen.Router.NavigateAndReset.Execute(new LoginViewModel(Locator.Current.GetService<IScreen>()));
            return Unit.Default;
        }

        private Task<bool> OpenMenuImpl()
        {
            IsSideNavigationVisibleSubject.OnNext(true);
            IsSideNavigationVisible = true;
#if WINDOWS_PHONE_APP || WINDOWS_UWP
#if WINDOWS_UWP
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.PhoneContract"))
                {
#endif
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
#if WINDOWS_UWP
                }
#endif
#endif
            return Task.FromResult(IsSideNavigationVisible);
        }
        
        private Task<bool> CloseMenuImpl()
        {
            IsSideNavigationVisibleSubject.OnNext(false);
            IsSideNavigationVisible = false;
#if WINDOWS_PHONE_APP
            SetCoreWindowBounds(CurrentViewModelType);
#endif
            return Task.FromResult(IsSideNavigationVisible);
        }

        private void SetCurrentViewModelImpl(IRoutableViewModel currentVm)
        {
            // Current Page Set
            CurrentPage = Locator.Current.GetService<ResourceLoader>().GetString(currentVm.UrlPathSegment);
            CurrentViewModelType = currentVm.GetType();
#if WINDOWS_PHONE_APP
            SetCoreWindowBounds(CurrentViewModelType);
#endif
            // Highlights the corresponding current ViewModel
            IsMain = MainTypes.Contains(CurrentViewModelType);
            IsSchedule = ScheduleTypes.Contains(CurrentViewModelType);
            IsGrade = GradeTypes.Contains(CurrentViewModelType);
            IsProgram = ProgramTypes.Contains(CurrentViewModelType);
            IsMoodle = MoodleTypes.Contains(CurrentViewModelType);
            IsSettings = SettingsTypes.Contains(CurrentViewModelType);
        }

        private IObservable<object> NavigateToViewModelImpl(object type)
        {
            var navType = type as Type;
            if (navType != CurrentViewModelType)
            {
                return Observable.Return(Activator.CreateInstance(navType, Screen));
            }
            return Observable.Empty<object>();
        }

#if WINDOWS_PHONE_APP
        private void SetCoreWindowBounds(Type vmType)
        {
            try
            {
                ApplicationView.GetForCurrentView()
                    .SetDesiredBoundsMode(vmType == typeof(ScheduleViewModel)
                        ? ApplicationViewBoundsMode.UseCoreWindow
                        : ApplicationViewBoundsMode.UseVisible);
            }
            catch (Exception ex)
            {
                this.Log().Error($"Cannot set the application bounds: {ex.Message}");
            }
        }
#endif

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
        
        public ReactiveCommand<object> NavCommand { get; private set; }

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

        /// <summary>
        /// Allows the Visibility of the sidenavigation to be observed and observable
        /// </summary>
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

        private bool _isMoodle;
        public bool IsMoodle
        {
            get { return _isMoodle; }
            set { this.RaiseAndSetIfChanged(ref _isMoodle, value); }
        }

        public Type[] MoodleTypes { get; } = { typeof(MoodleMainPageViewModel) };

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