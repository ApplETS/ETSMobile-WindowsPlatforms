using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Entities.Moodle;
using System;

namespace Ets.Mobile.Client.Contracts
{
    public interface IMoodleService
    {
        void SetCredentials(EtsUserCredentials vm);
        IObservable<MoodleSiteInfoVm> SiteInfo();
        IObservable<MoodleCourseVm[]> Courses();
        IObservable<MoodleCourseContentVm[]> CoursesContents(int courseId);
    }
}