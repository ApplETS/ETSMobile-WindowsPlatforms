using System;

namespace Ets.Mobile.Client
{
    public static class ClientKeys
    {
        /// <summary>
        /// <para>arg1 Semester</para>
        /// <para>arg2 Course</para>
        /// </summary>
        public static Func<string, string, string> ColorCourseForSemester = (semester, course) => "colorsFor_" + semester + "_" + course;
    }
}
