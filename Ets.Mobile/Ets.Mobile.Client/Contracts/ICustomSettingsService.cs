using System;
using System.Threading.Tasks;
using Ets.Mobile.Entities.Signets.Interfaces;

namespace Ets.Mobile.Client.Contracts
{
    public interface ICustomSettingsService
    {
        /// <summary>
        /// Apply The Color of Each schedule items according
        /// to the Course's Name and Semester
        /// </summary>
        /// <param name="schedules">Schedule Items</param>
        /// <param name="semester">Semester in-which the class are</param>
        /// <param name="courseNameSelector">Course name Selector</param>
        /// <returns></returns>
        Task<T[]> ApplyColorOnCoursesForSemester<T>(T[] schedules, string semester, Func<T, object> courseNameSelector) where T : class, ICustomColor;
    }
}
