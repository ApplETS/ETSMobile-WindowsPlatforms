using Ets.Mobile.Business.Entities.Signets;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface ICourseForSemester
    {
        List<CourseForSemester> CoursesForSemester { get; set; }
        string ErrorMessage { get; set; }
    }
}