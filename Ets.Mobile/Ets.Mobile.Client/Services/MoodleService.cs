using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Moodle.Courses;
using Ets.Mobile.Business.Entities.Moodle.CoursesContent;
using Ets.Mobile.Business.Entities.Moodle.SiteInfo;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Entities.Moodle;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using Windows.ApplicationModel.Resources;

namespace Ets.Mobile.Client.Services
{
    public class MoodleService : IMoodleService
    {
        private EtsUserCredentials _userCredentials;
        private readonly IMoodleBusinessService _services;
        private readonly MoodleAbstractFactory _factory;

        public MoodleService(IMoodleBusinessService services, MoodleAbstractFactory factory)
        {
            _services = services;
            _factory = factory;
        }

        public void SetCredentials(EtsUserCredentials userCredentials)
        {
            _userCredentials = userCredentials;
        }

        private IObservable<string> GetToken()
        {
            return _services.Token(_userCredentials.Username, _userCredentials.Password)
                .Select(t =>
                {
                    HandleMoodleExceptions(Locator.Current.GetService<ResourceLoader>().GetString("MoodleInvalidToken"), t.Error, t.DebugInfo, t.ReproductionLink, t.StackTrace);
                    return t.Token;
                });
        }

        public IObservable<MoodleSiteInfoVm> SiteInfo()
        {
            return GetToken()
                .SelectMany(token => _services.SiteInfo(token))
                .Select(si =>
                {
                    HandleMoodleExceptions(Locator.Current.GetService<ResourceLoader>().GetString("MoodleSiteInfoEmpty"), si.ErrorCode, si.Exception, si.Message);
                    return _factory.CreateFor<MoodleSiteInfo, MoodleSiteInfoVm>(si);
                });
        }

        public IObservable<MoodleCourseVm[]> Courses()
        {
            return GetToken()
                .CombineLatest(SiteInfo(), (token, si) => new Tuple<string, MoodleSiteInfoVm>(token, si))
                .SelectMany(tokenAndSiteInfo => _services.Courses(tokenAndSiteInfo.Item1, tokenAndSiteInfo.Item2.UserId))
                .Select(si =>
                {
                    HandleMoodleExceptions(si, Locator.Current.GetService<ResourceLoader>().GetString("MoodleCourseEmpty"));
                    return _factory.CreateFor<MoodleCourse[], MoodleCourseVm[]>(si);
                });
        }

        public IObservable<MoodleCourseContentVm[]> CoursesContents(int courseId)
        {
            return GetToken()
                .SelectMany(token => _services.CoursesContents(token, courseId))
                .Select(cc =>
                {
                    HandleMoodleExceptions(cc, Locator.Current.GetService<ResourceLoader>().GetString("MoodleCourseContentEmpty"));
                    return _factory.CreateFor<MoodleCourseContent[], MoodleCourseContentVm[]>(cc);
                });
        }

        private void HandleMoodleExceptions(string exceptionMessage, params string[] strings)
        {
            strings?.Where(str => !string.IsNullOrEmpty(str)).ToObservable().Do(x =>
            {
                throw new MoodleException(exceptionMessage);
            });
        }

        private void HandleMoodleExceptions(object[] objects, string exceptionMessage)
        {
            if(objects == null || objects.Length == 0)
            {
                throw new MoodleException(exceptionMessage);
            }
        }
    }
}