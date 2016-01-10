using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Interfaces.Moodle;

namespace Ets.Mobile.Client.Factories.Implementations.Moodle
{
    public class MoodleFactory : MoodleAbstractFactory 
    {
        private IMoodleSiteInfoFactory _moodleSiteInfoFactory;
        public override IMoodleSiteInfoFactory GetMoodleSiteInfoFactory()
        {
            return _moodleSiteInfoFactory ?? (_moodleSiteInfoFactory = new MoodleSiteInfoFactory());
        }

        private IMoodleCourseFactory _moodleCourseFactory;
        public override IMoodleCourseFactory GetMoodleCourseFactory()
        {
            return _moodleCourseFactory ?? (_moodleCourseFactory = new MoodleCourseFactory());
        }

        private IMoodleCourseContentFactory _moodleCourseContentFactory;
        public override IMoodleCourseContentFactory GetMoodleCourseContentFactory()
        {
            return _moodleCourseContentFactory ?? (_moodleCourseContentFactory = new MoodleCourseContentFactory());
        }
    }
}