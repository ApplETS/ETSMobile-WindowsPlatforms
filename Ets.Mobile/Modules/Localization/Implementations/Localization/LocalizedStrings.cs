
using Localization.Interface.Contracts;
using Splat;
namespace Localization
{
    public sealed class StringResources
    {
        private static readonly IResourceContainer ResourceLoader = Locator.Current.GetService<IResourceContainer>(); 

        public static string ActualGrade  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ActualGrade");
            }
        } 
        public static string ActualGradeUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ActualGradeUid");
            }
        } 
        public static string ApplicationNameUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ApplicationNameUid");
            }
        } 
        public static string ApplicationTitleUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ApplicationTitleUid");
            }
        } 
        public static string Autumn  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Autumn");
            }
        } 
        public static string Average  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Average");
            }
        } 
        public static string AverageUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("AverageUid");
            }
        } 
        public static string ConnectButtonUid_Content  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ConnectButtonUid");
            }
        } 
        public static string ConnectUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ConnectUid");
            }
        } 
        public static string Grade  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Grade");
            }
        } 
        public static string GradesEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("GradesEmpty");
            }
        } 
        public static string GradeUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("GradeUid");
            }
        } 
        public static string Median  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Median");
            }
        } 
        public static string MedianUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MedianUid");
            }
        } 
        public static string MinutesRemainingUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MinutesRemainingUid");
            }
        } 
        public static string MyGradesUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MyGradesUid");
            }
        } 
        public static string NetworkError  
        {
            get
            {
                return ResourceLoader.GetStringForKey("NetworkError");
            }
        } 
        public static string NetworkTitleError  
        {
            get
            {
                return ResourceLoader.GetStringForKey("NetworkTitleError");
            }
        } 
        public static string NewsHeaderUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("NewsHeaderUid");
            }
        } 
        public static string No  
        {
            get
            {
                return ResourceLoader.GetStringForKey("No");
            }
        } 
        public static string PasswordUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("PasswordUid");
            }
        } 
        public static string Percentile  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Percentile");
            }
        } 
        public static string PercentileUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("PercentileUid");
            }
        } 
        public static string ProfileHeaderUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ProfileHeaderUid");
            }
        } 
        public static string ScheduleEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleEmpty");
            }
        } 
        public static string StandardDeviation  
        {
            get
            {
                return ResourceLoader.GetStringForKey("StandardDeviation");
            }
        } 
        public static string StandardDeviationUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("StandardDeviationUid");
            }
        } 
        public static string StudentIsInactiveInCurrentSemester  
        {
            get
            {
                return ResourceLoader.GetStringForKey("StudentIsInactiveInCurrentSemester");
            }
        } 
        public static string SummaryUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SummaryUid");
            }
        } 
        public static string Summer  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Summer");
            }
        } 
        public static string TodayEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("TodayEmpty");
            }
        } 
        public static string TodayUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("TodayUid");
            }
        } 
        public static string UsernameUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("UsernameUid");
            }
        } 
        public static string WeightingUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("WeightingUid");
            }
        } 
        public static string Winter  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Winter");
            }
        } 
        public static string Yes  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Yes");
            }
        } 
        public static string Hour  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Hour");
            }
        } 
        public static string Hours  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Hours");
            }
        } 
        public static string Minute  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Minute");
            }
        } 
        public static string Minutes  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Minutes");
            }
        } 
        public static string CalendarUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CalendarUid");
            }
        } 
        public static string GradesUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("GradesUid");
            }
        } 
        public static string ProgramUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ProgramUid");
            }
        } 
        public static string ProgramEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ProgramEmpty");
            }
        } 
        public static string HomeUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("HomeUid");
            }
        } 
        public static string ScheduleUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleUid");
            }
        } 
        public static string SettingsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SettingsUid");
            }
        } 
        public static string Grades  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Grades");
            }
        } 
        public static string Home  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Home");
            }
        } 
        public static string Program  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Program");
            }
        } 
        public static string Schedule  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Schedule");
            }
        } 
        public static string SelectCourse  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SelectCourse");
            }
        } 
        public static string LogoutUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LogoutUid");
            }
        } 
        public static string ToBeDetermined  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ToBeDetermined");
            }
        } 
        public static string AboutUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("AboutUid");
            }
        } 
        public static string Settings  
        {
            get
            {
                return ResourceLoader.GetStringForKey("Settings");
            }
        } 
        public static string AppletsDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("AppletsDescriptionUid");
            }
        } 
        public static string AppletsWebsiteUid_NavigateUri  
        {
            get
            {
                return ResourceLoader.GetStringForKey("AppletsWebsiteUid");
            }
        } 
        public static string ContributeDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ContributeDescriptionUid");
            }
        } 
        public static string EcoledeTechnologieSuperieurUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("EcoledeTechnologieSuperieurUid");
            }
        } 
        public static string VisitAndJoinUsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("VisitAndJoinUsUid");
            }
        } 
        public static string AkavacheDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("AkavacheDescriptionUid");
            }
        } 
        public static string CreditsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CreditsUid");
            }
        } 
        public static string CrittercismDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CrittercismDescriptionUid");
            }
        } 
        public static string ModernHttpClientDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ModernHttpClientDescriptionUid");
            }
        } 
        public static string NewtonsoftJsonDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("NewtonsoftJsonDescriptionUid");
            }
        } 
        public static string ReactiveExtensionsDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ReactiveExtensionsDescriptionUid");
            }
        } 
        public static string ReactiveUIDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ReactiveUIDescriptionUid");
            }
        } 
        public static string RefitDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RefitDescriptionUid");
            }
        } 
        public static string SplatDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SplatDescriptionUid");
            }
        } 
        public static string SqliteDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SqliteDescriptionUid");
            }
        } 
        public static string SyncfusionDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SyncfusionDescriptionUid");
            }
        } 
        public static string ConfidentialityUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ConfidentialityUid");
            }
        } 
        public static string FeedbackButtonUid_Content  
        {
            get
            {
                return ResourceLoader.GetStringForKey("FeedbackButtonUid");
            }
        } 
        public static string GradesNotAvailable  
        {
            get
            {
                return ResourceLoader.GetStringForKey("GradesNotAvailable");
            }
        } 
        public static string ScheduleEmptyMessage  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleEmptyMessage");
            }
        } 
        public static string ScheduleEmptyTitle  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleEmptyTitle");
            }
        } 
        public static string PaulBettsContributionsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("PaulBettsContributionsUid");
            }
        } 
        public static string FusilladeDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("FusilladeDescriptionUid");
            }
        } 
        public static string ApplicationLoadingUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ApplicationLoadingUid");
            }
        } 
        public static string ExtendedSplashScreen  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ExtendedSplashScreen");
            }
        } 
        public static string SendLogFilesBody  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SendLogFilesBody");
            }
        } 
        public static string SendLogFilesSubject  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SendLogFilesSubject");
            }
        } 
        public static string SendLogsButtonUid_Content  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SendLogsButtonUid");
            }
        } 
        public static string MoodleMainPage  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleMainPage");
            }
        } 
        public static string MoodleCoursesEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCoursesEmpty");
            }
        } 
        public static string MoodleCourseContent  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCourseContent");
            }
        } 
        public static string MoodleCoursesContentEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCoursesContentEmpty");
            }
        } 
        public static string MoodleCoursesModuleEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCoursesModuleEmpty");
            }
        } 
        public static string MoodleCoursesModuleContentEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCoursesModuleContentEmpty");
            }
        } 
        public static string MoodleCourseModule  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCourseModule");
            }
        } 
        public static string MoodleCourseModuleContent  
        {
            get
            {
                return ResourceLoader.GetStringForKey("MoodleCourseModuleContent");
            }
        } 
        public static string CourseModuleContentUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CourseModuleContentUid");
            }
        } 
        public static string CourseMoodleUrlUid_Source  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CourseMoodleUrlUid");
            }
        } 
        public static string LinkToCourseModuleUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LinkToCourseModuleUid");
            }
        } 
        public static string RefreshUid_Label  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RefreshUid");
            }
        } 
        public static string ViewCalendarUid_Label  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ViewCalendarUid");
            }
        } 
        public static string LiveTileAndLockScreenUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LiveTileAndLockScreenUid");
            }
        } 
        public static string LiveTileAndLockScreenToggleSwitchUid_OffContent  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LiveTileAndLockScreenToggleSwitchUid");
            }
        } 
        public static string LiveTileAndLockScreenToggleSwitchUid_OnContent  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LiveTileAndLockScreenToggleSwitchUid");
            }
        } 
        public static string LiveTileAndLockScreenDescription1Uid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LiveTileAndLockScreenDescription1Uid");
            }
        } 
        public static string LiveTileAndLockScreenDescription2Uid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LiveTileAndLockScreenDescription2Uid");
            }
        } 
        public static string LiveTileAndLockScreenLinkToLockScreenUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LiveTileAndLockScreenLinkToLockScreenUid");
            }
        } 
        public static string OptionsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("OptionsUid");
            }
        } 
        public static string CalendarIntegrationDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CalendarIntegrationDescriptionUid");
            }
        } 
        public static string CalendarIntegrationUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CalendarIntegrationUid");
            }
        } 
        public static string RemoveScheduleIntegrationButtonUid_Content  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationButtonUid");
            }
        } 
        public static string RemoveScheduleIntegrationMessageCompleted  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationMessageCompleted");
            }
        } 
        public static string RemoveScheduleIntegrationMessageException  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationMessageException");
            }
        } 
        public static string RemoveScheduleIntegrationMessageNoCalendar  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationMessageNoCalendar");
            }
        } 
        public static string RemoveScheduleIntegrationTitleCompleted  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationTitleCompleted");
            }
        } 
        public static string RemoveScheduleIntegrationTitleException  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationTitleException");
            }
        } 
        public static string RemoveScheduleIntegrationTitleNoCalendar  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RemoveScheduleIntegrationTitleNoCalendar");
            }
        } 
        public static string ScheduleCalendarName  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleCalendarName");
            }
        } 
        public static string ScheduleIntegrationButtonUid_Content  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationButtonUid");
            }
        } 
        public static string ScheduleIntegrationDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationDescriptionUid");
            }
        } 
        public static string ScheduleIntegrationMessageCompleted  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationMessageCompleted");
            }
        } 
        public static string ScheduleIntegrationMessageEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationMessageEmpty");
            }
        } 
        public static string ScheduleIntegrationMessageException  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationMessageException");
            }
        } 
        public static string ScheduleIntegrationTitleCompleted  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationTitleCompleted");
            }
        } 
        public static string ScheduleIntegrationTitleEmpty  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationTitleEmpty");
            }
        } 
        public static string ScheduleIntegrationTitleException  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationTitleException");
            }
        } 
        public static string ScheduleIntegrationUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ScheduleIntegrationUid");
            }
        } 
        public static string PinAppToStartDescriptionUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("PinAppToStartDescriptionUid");
            }
        } 
        public static string PinAppToStartUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("PinAppToStartUid");
            }
        } 
        public static string LoginInvalidCredentialsMessage  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LoginInvalidCredentialsMessage");
            }
        } 
        public static string LoginTimeoutMessage  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LoginTimeoutMessage");
            }
        } 
        public static string CompletedCreditsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CompletedCreditsUid");
            }
        } 
        public static string EquivalenceCountUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("EquivalenceCountUid");
            }
        } 
        public static string FailedCreditsCountUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("FailedCreditsCountUid");
            }
        } 
        public static string PotentialCreditsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("PotentialCreditsUid");
            }
        } 
        public static string RegisteredCreditsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("RegisteredCreditsUid");
            }
        } 
        public static string ResearchCreditsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("ResearchCreditsUid");
            }
        } 
        public static string SuceededCreditsCountUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("SuceededCreditsCountUid");
            }
        } 
        public static string LinkToCourseUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("LinkToCourseUid");
            }
        } 
        public static string CourseContentsUid_Text  
        {
            get
            {
                return ResourceLoader.GetStringForKey("CourseContentsUid");
            }
        } 
    }
}