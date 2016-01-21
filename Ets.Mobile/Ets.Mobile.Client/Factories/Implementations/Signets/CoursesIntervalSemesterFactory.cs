using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class CoursesIntervalSemesterFactory : ICoursesIntervalSemesterFactory
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