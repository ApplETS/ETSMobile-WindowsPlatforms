using Ets.Mobile.Business.Entities.Moodle.Courses;
using Ets.Mobile.Client.Factories.Interfaces.Shared;
using Ets.Mobile.Entities.Moodle;

namespace Ets.Mobile.Client.Factories.Interfaces.Moodle
{
    public interface IMoodleCourseFactory : IFactory<MoodleCourse[], MoodleCourseVm[]>
    {
    }
}