﻿using System;

namespace Ets.Mobile.ViewModel
{
    public static class ViewModelKeys
    {
        public const string Login = "login_credentials";
        public static Func<string, string> ScheduleForSemester = semester => "schedule_" + semester;
        public const string Semesters = "semesters";
        public const string Grades = "grades";
        public const string Courses = "courses";
        /// <summary>
        /// arg1: semester
        /// arg2: course
        /// </summary>
        public static Func<string, string, string> GradesForSemesterAndCourse = (semester, course) => "grades_" + semester + "_" + course;
        public static string UserProfile = "profile";
        public static string Gravatar = "gravatar";
    }
}
