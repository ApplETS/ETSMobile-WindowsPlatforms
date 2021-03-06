﻿using Ets.Mobile.Business.Entities.Signets;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface ICoursesIntervalSemester
    {
        List<Course> Courses { get; set; }
        string ErrorMessage { get; set; }
    }
}