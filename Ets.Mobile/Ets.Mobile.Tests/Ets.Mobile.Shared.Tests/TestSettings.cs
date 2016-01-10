﻿namespace Ets.Mobile.Shared.Tests
{
    public static class TestSettings
    {
        #region User Credentials
        // Do not commit this with real credentials. For test purposes only
        public static string Username = "";
        public static string Password = "";
        #endregion

        #region Courses
        public static string Semester = "A2015";
        public static string Course = "MAT350";
        public static string Group = "05";
        public static string CourseGroup = $"{Course}-{Group}";
        public static string DateDebut = ""; //new DateTime(2014, 09, 01).ToString("yyyy-MM-dd");
        public static string DateFin = ""; //new DateTime(2014, 12, 19).ToString("yyyy-MM-dd");
        #endregion
    }
}