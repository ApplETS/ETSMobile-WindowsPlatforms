using System;
using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class CoursesForSemesterFactory : ICoursesForSemesterFactory
    {
        public List<CourseForSemesterVm> Create(CourseForSemesterResult courseForSemester)
        {
            return new List<CourseForSemesterVm>(
                courseForSemester.CoursesForSemester.Select(cfs => new CourseForSemesterVm
                {
                    Acronym = cfs.Acronym,
                    Day = Convert.ToInt32(cfs.Day),
                    DayName = cfs.DayName,
                    EndHour = TimeSpan.Parse(cfs.EndHour),
                    Group = cfs.Group,
                    Name = cfs.Name,
                    Location = cfs.Location,
                    Title = cfs.Title,
                    Type = cfs.Type,
                    IsPrincipalActivity = cfs.IsPrincipalActivity == "Oui",
                    StartHour = TimeSpan.Parse(cfs.StartHour)
                }));
        }
    }
}
