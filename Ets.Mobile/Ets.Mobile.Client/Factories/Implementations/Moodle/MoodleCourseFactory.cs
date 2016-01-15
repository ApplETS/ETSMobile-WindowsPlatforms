using Ets.Mobile.Business.Entities.Moodle.Courses;
using Ets.Mobile.Client.Factories.Interfaces.Moodle;
using Ets.Mobile.Entities.Moodle;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ets.Mobile.Client.Factories.Implementations.Moodle
{
    public class MoodleCourseFactory : IMoodleCourseFactory
    {
        private const string PatternGroup1 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})$";
        private const string PatternGroup2 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})$";
        private const string PatternGroup3 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})-(?<group3>\d{2})$";
        private const string PatternGroup4 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})-(?<group3>\d{2})-(?<group4>\d{2})$";
        private const string PatternGroup5 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})-(?<group3>\d{2})-(?<group4>\d{2})-(?<group5>\d{2})$";
        private const string CourseEmptySummary = "<p>{Ce résumé apparaît dans la liste des cours. Mettez ici une description concise mais PAS la description de l'annuaire.}</p>";

        public MoodleCourseVm[] Create(MoodleCourse[] result)
        {
            return result.Select(mc =>
            {
                var courseDesc = GetCourseDescriptionFromShortName(mc.ShortName);
                return new MoodleCourseVm
                {
                    Id = mc.Id,
                    ShortName = mc.ShortName,
                    FullName = mc.FullName,
                    IdNumber = mc.IdNumber,
                    Summary = !string.IsNullOrEmpty(mc.Summary) && mc.Summary != CourseEmptySummary ? mc.Summary : string.Empty,
                    Semester = courseDesc.Semester,
                    CourseName = courseDesc.CourseName,
                    Groups = courseDesc.Groups
                };
            }).ToArray();
        }

        private class MoodleCourseDescription
        {
            public string Semester { get; set; }
            public string CourseName { get; set; }
            public string[] Groups { get; set; }
        }

        private static MoodleCourseDescription GetCourseDescriptionFromShortName(string shortName)
        {
            if (Regex.IsMatch(shortName, PatternGroup5))
            {
                var groups = Regex.Match(shortName, PatternGroup5);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value, groups.Groups["group3"].Value, groups.Groups["group4"].Value, groups.Groups["group5"].Value }
                };
            }

            if (Regex.IsMatch(shortName, PatternGroup4))
            {
                var groups = Regex.Match(shortName, PatternGroup4);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value, groups.Groups["group3"].Value, groups.Groups["group4"].Value }
                };
            }

            if (Regex.IsMatch(shortName, PatternGroup3))
            {
                var groups = Regex.Match(shortName, PatternGroup3);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value, groups.Groups["group3"].Value }
                };
            }

            if (Regex.IsMatch(shortName, PatternGroup2))
            {
                var groups = Regex.Match(shortName, PatternGroup2);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value }
                };
            }

            if (Regex.IsMatch(shortName, PatternGroup1))
            {
                var groups = Regex.Match(shortName, PatternGroup1);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value }
                };
            }
            
            return new MoodleCourseDescription
            {
                CourseName = shortName.Substring(0, shortName.IndexOf('-')),
                Semester = "N/A",
                Groups = new string[] {}
            };
        }
    }
}