
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
        public static string GradeHeaderUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("GradeHeaderUid");
            }
        } 
        public static string GradesEmptyUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("GradesEmptyUid");
            }
        } 
        public static string GradeTitleUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("GradeTitleUid");
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
        public static string ScheduleEmptyUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("ScheduleEmptyUid");
            }
        } 
        public static string SelectProgram  
        {
            get
            {
                return _resourceLoader.GetString("SelectProgram");
            }
        } 
        public static string SelectProgramUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("SelectProgramUid");
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
        public static string TodayEmptyUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("TodayEmptyUid");
            }
        } 
        public static string TodayHeaderUid_Text  
        {
            get
            {
                return _resourceLoader.GetString("TodayHeaderUid");
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
    }
}