using Ets.Mobile.Business.Entities.Moodle.CoursesContent;
using Ets.Mobile.Client.Factories.Interfaces.Shared;
using Ets.Mobile.Entities.Moodle;

namespace Ets.Mobile.Client.Factories.Interfaces.Moodle
{
    public interface IMoodleCourseContentFactory : IFactory<MoodleCourseContent[], MoodleCourseContentVm[]>
    {
    }
}