﻿using System;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Moodle.Courses;
using Ets.Mobile.Business.Entities.Moodle.CoursesContent;
using Ets.Mobile.Business.Entities.Moodle.SiteInfo;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Entities.Moodle;
using Splat;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Akavache;

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

        public void SetCredentials(EtsUserCredentials credentials)
        {
            _userCredentials = credentials;
        }

        private async  Task<string> GetToken()
        {
            var token = await _services.Token(_userCredentials.Username, _userCredentials.Password);

            HandleMoodleExceptions(Locator.Current.GetService<ResourceLoader>().GetString("MoodleInvalidToken"), token.Error, token.DebugInfo, token.ReproductionLink, token.StackTrace);

            return token.Token;
        }

        public async Task<MoodleSiteInfoVm> SiteInfo(string token = "", bool loadUserPicture = false)
        {
            if (string.IsNullOrEmpty(token))
            {
                token = await GetToken();
            }

            var siteInfo = await _services.SiteInfo(token);

            HandleMoodleExceptions(Locator.Current.GetService<ResourceLoader>().GetString("MoodleSiteInfoEmpty"), siteInfo.ErrorCode, siteInfo.Exception, siteInfo.Message);

            var vm = _factory.CreateFor<MoodleSiteInfo, MoodleSiteInfoVm>(siteInfo);

            if (loadUserPicture)
            {
                return await BlobCache.UserAccount.LoadImageFromUrl("moodle_userpicture", vm.UserPictureUrl, true).Select(image =>
                {
                    vm.UserPicture = image;
                    return vm;
                }).ToTask();
            }

            return vm;
        }

        public async Task<MoodleCourseVm[]> Courses()
        {
            var token = await GetToken();

            var siteInfo = await SiteInfo(token);

            var courses = await _services.Courses(token, siteInfo.UserId);

            HandleMoodleExceptions(courses, Locator.Current.GetService<ResourceLoader>().GetString("MoodleCourseEmpty"));

            return _factory.CreateFor<MoodleCourse[], MoodleCourseVm[]>(courses);
        }

        public async Task<MoodleCourseContentVm[]> CoursesContents(int courseId)
        {
            var token = await GetToken();

            var courseContents = await _services.CoursesContents(token, courseId);
            
            HandleMoodleExceptions(courseContents, Locator.Current.GetService<ResourceLoader>().GetString("MoodleCourseContentEmpty"));

            return _factory.CreateFor<MoodleCourseContent[], MoodleCourseContentVm[]>(courseContents);
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