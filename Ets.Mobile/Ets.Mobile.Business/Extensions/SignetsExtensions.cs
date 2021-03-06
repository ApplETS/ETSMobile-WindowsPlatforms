﻿using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ets.Mobile.Business.Extensions
{
    public static class SignetsApiExtensions
    {
        private static Dictionary<string, string> AuthentificatedDictionary(string username, string password)
        {
            return new Dictionary<string, string>
            {
                {"codeAccesUniversel", username},
                {"motPasse", password}
            };
        }

        private static Dictionary<string, string> Append(this Dictionary<string, string> dict, string key, string value)
        {
            dict.Add(key, value);

            return dict;
        }

        public static Task<LoginResult> Login(this ISignetsBusinessService This, string userName, string password)
        {
            var dict = AuthentificatedDictionary(userName, password);

            return This.LoginRaw(dict);
        }

        public static Task<UserDetailsResult> UserDetails(this ISignetsBusinessService This, string userName, string password)
        {
            var dict = AuthentificatedDictionary(userName, password);

            return This.UserDetailsRaw(dict);
        }

        public static Task<CoursesResult> Courses(this ISignetsBusinessService This, string userName, string password)
        {
            var dict = AuthentificatedDictionary(userName, password);

            return This.CoursesRaw(dict);
        }

        public static Task<CoursesIntervalSemesterResult> CoursesIntervalSemester(this ISignetsBusinessService This, string userName, string password, string startSemester, string endSemester)
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("SesDebut", startSemester)
                .Append("SesFin", endSemester);

            return This.CoursesIntervalSemesterRaw(dict);
        }

        public static Task<SemestersResult> Semesters(this ISignetsBusinessService This, string userName, string password)
        {
            var dict = AuthentificatedDictionary(userName, password);

            return This.SemestersRaw(dict);
        }

        public static Task<ProgramsResult> Programs(this ISignetsBusinessService This, string userName, string password)
        {
            var dict = AuthentificatedDictionary(userName, password);

            return This.ProgramsRaw(dict);
        }

        public static Task<TeammatesResult> Teammates(this ISignetsBusinessService This, string userName, string password, string courseAbridgedName, string group, string semesterAbridgedName, string evaluationElementName = "")
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("pSigle", courseAbridgedName)
                .Append("pGroupe", group)
                .Append("pSession", semesterAbridgedName)
                .Append("pNomElementEval", evaluationElementName);

            return This.TeammatesRaw(dict);
        }

        public static Task<EvaluationsResult> Evaluations(this ISignetsBusinessService This, string userName, string password, string courseAbridgedName, string group, string semesterAbridgedName)
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("pSigle", courseAbridgedName)
                .Append("pGroupe", group)
                .Append("pSession", semesterAbridgedName);

            return This.EvaluationsRaw(dict);
        }

        public static Task<ScheduleAndTeachersResult> ScheduleAndTeachers(this ISignetsBusinessService This, string userName, string password, string semesterAbridgedName)
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("pSession", semesterAbridgedName);

            return This.ScheduleAndTeachersRaw(dict);
        }

        public static Task<ScheduleFinalExamsResult> ScheduleFinalExams(this ISignetsBusinessService This, string userName, string password, string semesterAbridgedName)
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("pSession", semesterAbridgedName);

            return This.ScheduleFinalExamsRaw(dict);
        }

        public static Task<CourseForSemesterResult> CoursesForSemester(this ISignetsBusinessService This, string userName, string password, string semesterAbridgedName, string courseAbridgedName)
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("pSession", semesterAbridgedName)
                .Append("prefixeSigleCours", courseAbridgedName);

            return This.CoursesForSemesterRaw(dict);
        }

        public static Task<ReplacedDaysResult> ReplacedDays(this ISignetsBusinessService This, string semesterAbridgedName)
        {
            var dict = new Dictionary<string, string> {
                { "pSession", semesterAbridgedName }
            };

            return This.ReplacedDaysRaw(dict);
        }

        public static Task<ScheduleResult> Schedule(this ISignetsBusinessService This, string userName, string password, string semesterAbridgedName, string courseAbridgedNameAndGroup = "", string startDate = "", string endDate = "")
        {
            var dict = AuthentificatedDictionary(userName, password)
                .Append("pSession", semesterAbridgedName)
                .Append("pCoursGroupe", courseAbridgedNameAndGroup)
                .Append("pDateDebut", startDate)
                .Append("pDateFin", endDate);

            return This.ScheduleRaw(dict);
        }
    }
}