using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class CoursesFactory : ICoursesFactory
    {
        public List<CourseVm> Create(CoursesResult courses)
        {
            return new List<CourseVm>(
                courses.Courses.Select(course => new CourseVm
                {
                    Acronym = course.Acronym,
                    Semester = course.Semester,
                    Name = course.Name,
                    Group = course.Group,
                    Program = course.Program,
                    Credits = course.Credits,
                    Grade = course.Grade
                }));
        }
    }
}