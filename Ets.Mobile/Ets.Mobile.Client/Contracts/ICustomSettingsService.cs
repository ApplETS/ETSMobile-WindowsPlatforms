using Ets.Mobile.Entities.Moodle;
using Ets.Mobile.Entities.Signets;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Contracts
{
    public interface ICustomSettingsService
    {
        /// <summary>
        /// Apply the colors on the courses upon
        /// a successful login
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task ApplyColorOnItemsForCoursesInitial(CourseVm[] items);
        
        /// <summary>
        /// Apply The Color of Each Schedule items
        /// </summary>
        Task ApplyColorOnItemsForSchedule(ScheduleVm[] items);

        /// <summary>
        /// Apply The Color of Each Evaluations items
        /// </summary>
        Task ApplyColorOnItemsForEvaluations(EvaluationsVm items, string semester, string courseAndGroup);

        /// <summary>
        /// Apply The Color of Each Moodle courses
        /// </summary>
        Task ApplyColorOnItemsForMoodleCourses(MoodleCourseVm[] items);

        /// <summary>
        /// Apply The Color of Each Moodle course's content
        /// </summary>
        Task ApplyColorOnItemsForMoodleCourseContents(MoodleCourseContentVm[] items, MoodleCourseVm course);
    }
}