using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Interfaces
{
    public interface ICoursesForSemesterFactory : IFactory<CourseForSemesterResult, List<CourseForSemesterVm>>
    {
    }
}