
using Windows.ApplicationModel.Resources;
using Splat;
using System;
namespace Localization
{
    public sealed class StringResources
    {
        private readonly static ResourceLoader _resourceLoader = Locator.Current.GetService<ResourceLoader>(); 

        public static string ActualGrade  
        {
            get
            {
                return _resourceLoader.GetString("ActualGrade");
            }
        } 
        public static string ActualGradeUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ActualGradeUid");
            }
        } 
        public static string ApplicationNameUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ApplicationNameUid");
            }
        } 
        public static string ApplicationTitleUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ApplicationTitleUid");
            }
        } 
        public static string Autumn  
        {
            get
            {
                return _resourceLoader.GetString("Autumn");
            }
        } 
        public static string Average  
        {
            get
            {
                return _resourceLoader.GetString("Average");
            }
        } 
        public static string AverageUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("AverageUid");
            }
        } 
        public static string ConnectButtonUid_Content  
        {
            get
            {
                return _resourceLoader.GetString("ConnectButtonUid");
            }
        } 
        public static string ConnectUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ConnectUid");
            }
        } 
        public static string Grade  
        {
            get
            {
                return _resourceLoader.GetString("Grade");
            }
        } 
        public static string GradesEmpty  
        {
            get
            {
                return _resourceLoader.GetString("GradesEmpty");
            }
        } 
        public static string GradeUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("GradeUid");
            }
        } 
        public static string Median  
        {
            get
            {
                return _resourceLoader.GetString("Median");
            }
        } 
        public static string MedianUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("MedianUid");
            }
        } 
        public static string MinutesRemainingUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("MinutesRemainingUid");
            }
        } 
        public static string MyGradesUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("MyGradesUid");
            }
        } 
        public static string NetworkError  
        {
            get
            {
                return _resourceLoader.GetString("NetworkError");
            }
        } 
        public static string NetworkTitleError  
        {
            get
            {
                return _resourceLoader.GetString("NetworkTitleError");
            }
        } 
        public static string NewsHeaderUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("NewsHeaderUid");
            }
        } 
        public static string No  
        {
            get
            {
                return _resourceLoader.GetString("No");
            }
        } 
        public static string PasswordUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("PasswordUid");
            }
        } 
        public static string Percentile  
        {
            get
            {
                return _resourceLoader.GetString("Percentile");
            }
        } 
        public static string PercentileUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("PercentileUid");
            }
        } 
        public static string ProfileHeaderUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ProfileHeaderUid");
            }
        } 
        public static string ScheduleEmpty  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleEmpty");
            }
        } 
        public static string StandardDeviation  
        {
            get
            {
                return _resourceLoader.GetString("StandardDeviation");
            }
        } 
        public static string StandardDeviationUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("StandardDeviationUid");
            }
        } 
        public static string StudentIsInactiveInCurrentSemester  
        {
            get
            {
                return _resourceLoader.GetString("StudentIsInactiveInCurrentSemester");
            }
        } 
        public static string SummaryUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("SummaryUid");
            }
        } 
        public static string Summer  
        {
            get
            {
                return _resourceLoader.GetString("Summer");
            }
        } 
        public static string TodayEmpty  
        {
            get
            {
                return _resourceLoader.GetString("TodayEmpty");
            }
        } 
        public static string TodayUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("TodayUid");
            }
        } 
        public static string UsernameUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("UsernameUid");
            }
        } 
        public static string WeightingUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("WeightingUid");
            }
        } 
        public static string Winter  
        {
            get
            {
                return _resourceLoader.GetString("Winter");
            }
        } 
        public static string Yes  
        {
            get
            {
                return _resourceLoader.GetString("Yes");
            }
        } 
        public static string Hour  
        {
            get
            {
                return _resourceLoader.GetString("Hour");
            }
        } 
        public static string Hours  
        {
            get
            {
                return _resourceLoader.GetString("Hours");
            }
        } 
        public static string Minute  
        {
            get
            {
                return _resourceLoader.GetString("Minute");
            }
        } 
        public static string Minutes  
        {
            get
            {
                return _resourceLoader.GetString("Minutes");
            }
        } 
        public static string CalendarUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("CalendarUid");
            }
        } 
        public static string GradesUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("GradesUid");
            }
        } 
        public static string ProgramUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ProgramUid");
            }
        } 
        public static string ProgramEmpty  
        {
            get
            {
                return _resourceLoader.GetString("ProgramEmpty");
            }
        } 
        public static string HomeUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("HomeUid");
            }
        } 
        public static string ScheduleUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleUid");
            }
        } 
        public static string SettingsUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("SettingsUid");
            }
        } 
        public static string Grades  
        {
            get
            {
                return _resourceLoader.GetString("Grades");
            }
        } 
        public static string Home  
        {
            get
            {
                return _resourceLoader.GetString("Home");
            }
        } 
        public static string Program  
        {
            get
            {
                return _resourceLoader.GetString("Program");
            }
        } 
        public static string Schedule  
        {
            get
            {
                return _resourceLoader.GetString("Schedule");
            }
        } 
        public static string SelectCourse  
        {
            get
            {
                return _resourceLoader.GetString("SelectCourse");
            }
        } 
        public static string LogoutUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("LogoutUid");
            }
        } 
        public static string ToBeDetermined  
        {
            get
            {
                return _resourceLoader.GetString("ToBeDetermined");
            }
        } 
        public static string AboutUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("AboutUid");
            }
        } 
        public static string Settings  
        {
            get
            {
                return _resourceLoader.GetString("Settings");
            }
        } 
        public static string AppletsDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("AppletsDescriptionUid");
            }
        } 
        public static string AppletsWebsiteUid_NavigateUri  
        {
            get
            {
                return _resourceLoader.GetString("AppletsWebsiteUid");
            }
        } 
        public static string ContributeDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ContributeDescriptionUid");
            }
        } 
        public static string EcoledeTechnologieSuperieurUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("EcoledeTechnologieSuperieurUid");
            }
        } 
        public static string VisitAndJoinUsUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("VisitAndJoinUsUid");
            }
        } 
        public static string AkavacheDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("AkavacheDescriptionUid");
            }
        } 
        public static string CreditsUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("CreditsUid");
            }
        } 
        public static string CrittercismDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("CrittercismDescriptionUid");
            }
        } 
        public static string ModernHttpClientDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ModernHttpClientDescriptionUid");
            }
        } 
        public static string NewtonsoftJsonDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("NewtonsoftJsonDescriptionUid");
            }
        } 
        public static string ReactiveExtensionsDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ReactiveExtensionsDescriptionUid");
            }
        } 
        public static string ReactiveUIDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ReactiveUIDescriptionUid");
            }
        } 
        public static string RefitDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("RefitDescriptionUid");
            }
        } 
        public static string SplatDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("SplatDescriptionUid");
            }
        } 
        public static string SqliteDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("SqliteDescriptionUid");
            }
        } 
        public static string SyncfusionDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("SyncfusionDescriptionUid");
            }
        } 
        public static string ConfidentialityUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ConfidentialityUid");
            }
        } 
        public static string FeedbackButtonUid_Content  
        {
            get
            {
                return _resourceLoader.GetString("FeedbackButtonUid");
            }
        } 
        public static string GradesNotAvailable  
        {
            get
            {
                return _resourceLoader.GetString("GradesNotAvailable");
            }
        } 
        public static string ScheduleEmptyMessage  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleEmptyMessage");
            }
        } 
        public static string ScheduleEmptyTitle  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleEmptyTitle");
            }
        } 
        public static string PaulBettsContributionsUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("PaulBettsContributionsUid");
            }
        } 
        public static string FusilladeDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("FusilladeDescriptionUid");
            }
        } 
        public static string ApplicationLoadingUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ApplicationLoadingUid");
            }
        } 
        public static string ExtendedSplashScreen  
        {
            get
            {
                return _resourceLoader.GetString("ExtendedSplashScreen");
            }
        } 
        public static string SendLogFilesBody  
        {
            get
            {
                return _resourceLoader.GetString("SendLogFilesBody");
            }
        } 
        public static string SendLogFilesSubject  
        {
            get
            {
                return _resourceLoader.GetString("SendLogFilesSubject");
            }
        } 
        public static string SendLogsButtonUid_Content  
        {
            get
            {
                return _resourceLoader.GetString("SendLogsButtonUid");
            }
        } 
        public static string MoodleMainPage  
        {
            get
            {
                return _resourceLoader.GetString("MoodleMainPage");
            }
        } 
        public static string MoodleCoursesEmpty  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCoursesEmpty");
            }
        } 
        public static string MoodleCourseContent  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCourseContent");
            }
        } 
        public static string MoodleCoursesContentEmpty  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCoursesContentEmpty");
            }
        } 
        public static string MoodleCoursesModuleEmpty  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCoursesModuleEmpty");
            }
        } 
        public static string MoodleCoursesModuleContentEmpty  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCoursesModuleContentEmpty");
            }
        } 
        public static string MoodleCourseModule  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCourseModule");
            }
        } 
        public static string MoodleCourseModuleContent  
        {
            get
            {
                return _resourceLoader.GetString("MoodleCourseModuleContent");
            }
        } 
        public static string CourseModuleContentUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("CourseModuleContentUid");
            }
        } 
        public static string CourseMoodleUrlUid_Source  
        {
            get
            {
                return _resourceLoader.GetString("CourseMoodleUrlUid");
            }
        } 
        public static string LinkToCourseModuleUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("LinkToCourseModuleUid");
            }
        } 
        public static string RefreshUid_Label  
        {
            get
            {
                return _resourceLoader.GetString("RefreshUid");
            }
        } 
        public static string ViewCalendarUid_Label  
        {
            get
            {
                return _resourceLoader.GetString("ViewCalendarUid");
            }
        } 
        public static string LiveTileAndLockScreenUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("LiveTileAndLockScreenUid");
            }
        } 
        public static string LiveTileAndLockScreenToggleSwitchUid_OffContent  
        {
            get
            {
                return _resourceLoader.GetString("LiveTileAndLockScreenToggleSwitchUid");
            }
        } 
        public static string LiveTileAndLockScreenToggleSwitchUid_OnContent  
        {
            get
            {
                return _resourceLoader.GetString("LiveTileAndLockScreenToggleSwitchUid");
            }
        } 
        public static string LiveTileAndLockScreenDescription1Uid_Text  
        {
            get
            {
                return _resourceLoader.GetString("LiveTileAndLockScreenDescription1Uid");
            }
        } 
        public static string LiveTileAndLockScreenDescription2Uid_Text  
        {
            get
            {
                return _resourceLoader.GetString("LiveTileAndLockScreenDescription2Uid");
            }
        } 
        public static string LiveTileAndLockScreenLinkToLockScreenUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("LiveTileAndLockScreenLinkToLockScreenUid");
            }
        } 
        public static string OptionsUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("OptionsUid");
            }
        } 
        public static string CalendarIntegrationDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("CalendarIntegrationDescriptionUid");
            }
        } 
        public static string CalendarIntegrationUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("CalendarIntegrationUid");
            }
        } 
        public static string RemoveScheduleIntegrationButtonUid_Content  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationButtonUid");
            }
        } 
        public static string RemoveScheduleIntegrationMessageCompleted  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationMessageCompleted");
            }
        } 
        public static string RemoveScheduleIntegrationMessageException  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationMessageException");
            }
        } 
        public static string RemoveScheduleIntegrationMessageNoCalendar  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationMessageNoCalendar");
            }
        } 
        public static string RemoveScheduleIntegrationTitleCompleted  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationTitleCompleted");
            }
        } 
        public static string RemoveScheduleIntegrationTitleException  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationTitleException");
            }
        } 
        public static string RemoveScheduleIntegrationTitleNoCalendar  
        {
            get
            {
                return _resourceLoader.GetString("RemoveScheduleIntegrationTitleNoCalendar");
            }
        } 
        public static string ScheduleCalendarName  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleCalendarName");
            }
        } 
        public static string ScheduleIntegrationButtonUid_Content  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationButtonUid");
            }
        } 
        public static string ScheduleIntegrationDescriptionUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationDescriptionUid");
            }
        } 
        public static string ScheduleIntegrationMessageCompleted  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationMessageCompleted");
            }
        } 
        public static string ScheduleIntegrationMessageEmpty  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationMessageEmpty");
            }
        } 
        public static string ScheduleIntegrationMessageException  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationMessageException");
            }
        } 
        public static string ScheduleIntegrationTitleCompleted  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationTitleCompleted");
            }
        } 
        public static string ScheduleIntegrationTitleEmpty  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationTitleEmpty");
            }
        } 
        public static string ScheduleIntegrationTitleException  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationTitleException");
            }
        } 
        public static string ScheduleIntegrationUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleIntegrationUid");
            }
        } 
    }
}