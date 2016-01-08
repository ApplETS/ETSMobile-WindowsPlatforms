using Ets.Mobile.Business.Entities.Moodle.Courses;
using Ets.Mobile.Client.Factories.Interfaces.Moodle;
using Ets.Mobile.Entities.Moodle;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ets.Mobile.Client.Factories.Implementations.Moodle
{
    public class MoodleCourseFactory : IMoodleCourseFactory
    {
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
                    Summary = mc.Summary,
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
            var patternGroup1 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})$";
            var patternGroup2 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})$";
            var patternGroup3 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})-(?<group3>\d{2})$";
            var patternGroup4 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})-(?<group3>\d{2})-(?<group4>\d{2})$";
            var patternGroup5 = @"^S(?<year>\d{4})(?<season>\d{1})-(?<acronym>\w{6})-(?<group1>\d{2})-(?<group2>\d{2})-(?<group3>\d{2})-(?<group4>\d{2})-(?<group5>\d{2})$";

            if (Regex.IsMatch(shortName, patternGroup5))
            {
                var groups = Regex.Match(shortName, patternGroup5);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value, groups.Groups["group3"].Value, groups.Groups["group4"].Value, groups.Groups["group5"].Value }
                };
            }

            if (Regex.IsMatch(shortName, patternGroup4))
            {
                var groups = Regex.Match(shortName, patternGroup4);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value, groups.Groups["group3"].Value, groups.Groups["group4"].Value }
                };
            }

            if (Regex.IsMatch(shortName, patternGroup3))
            {
                var groups = Regex.Match(shortName, patternGroup3);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value, groups.Groups["group3"].Value }
                };
            }

            if (Regex.IsMatch(shortName, patternGroup2))
            {
                var groups = Regex.Match(shortName, patternGroup2);
                return new MoodleCourseDescription
                {
                    CourseName = groups.Groups["acronym"].Value,
                    Semester = (groups.Groups["season"].Value == "1" ? "H" : groups.Groups["season"].Value == "2" ? "E" : "A") + groups.Groups["year"],
                    Groups = new[] { groups.Groups["group1"].Value, groups.Groups["group2"].Value }
                };
            }

            if (Regex.IsMatch(shortName, patternGroup1))
            {
                var groups = Regex.Match(shortName, patternGroup1);
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