using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class CoursesIntervalSemestersFactory : ICoursesIntervalSemestersFactory
    {
        public List<CourseIntervalVm> Create(CoursesIntervalSemesterResult coursesIntervalSemester)
        {
            return new List<CourseIntervalVm>(
                coursesIntervalSemester.Courses.Select(ci => new CourseIntervalVm
                {
                    Acronym = ci.Acronym,
                    Credits = ci.Credits,
                    Semester = ci.Semester,
                    Name = ci.Name,
                    Group = ci.Group,
                    Program = ci.Program,
                    Grade = ci.Grade
                }));
        }
    }
}
