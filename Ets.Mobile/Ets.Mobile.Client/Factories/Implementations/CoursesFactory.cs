﻿using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
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
