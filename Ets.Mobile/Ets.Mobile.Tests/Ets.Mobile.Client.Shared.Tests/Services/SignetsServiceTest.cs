using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations;
using Ets.Mobile.Client.Services;
using Ets.Mobile.Client.Shared.Tests.Contracts;
using Ets.Mobile.Entities.ServiceInfo;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Shared.Tests;
using Fusillade;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ModernHttpClient;
using Refit;
using Splat;

namespace Ets.Mobile.Client.Shared.Tests.Services
{
    [TestClass]
    public class SignetsServiceTest : DTBase, ISignetsServiceTest
    {
        public ISignetsService GetSignetsServices()
        {
            if(locator.GetService<ISignetsService>() == null)
            {
                locator.Register(() =>
                    new SignetsServiceInfo { Url = "https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx" },
                    typeof(SignetsServiceInfo)
                );
                var client = new HttpClient(new NativeMessageHandler())
                {
                    BaseAddress = new Uri("https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx"),
                };
                locator.Register(() => RestService.For<ISignetsBusinessService>(client), typeof(ISignetsBusinessService));
                locator.Register(() => new SignetsFactory(), typeof(SignetsAbstractFactory));
                locator.Register(() => new SignetsService(locator.GetService<ISignetsBusinessService>(), locator.GetService<SignetsAbstractFactory>(), new SignetsAccountVm(TestSettings.Username, TestSettings.Password)), typeof(ISignetsService));
            }
            return locator.GetService<ISignetsService>();
        }

        [TestMethod]
        public async Task TestGetMyProfile()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var infos = await service.UserDetails();

            // Assert
            Assert.IsNotNull(infos);
        }
        
        [TestMethod]
        public async Task TestLogin()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var isLoggedIn = await service.Login(
                TestSettings.Username,
                TestSettings.Password
                );

            // Assert
            Assert.IsTrue(isLoggedIn);
        }

        [TestMethod]
        public async Task TestSchedule()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var sceances = await service.Schedule(
                TestSettings.Semester,
                TestSettings.CourseGroup
                );

            // Assert
            Assert.IsNotNull(sceances);
        }

        [TestMethod]
        public async Task TestReplacedDays()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var joursRemplaces = await service.ReplacedDays(
                TestSettings.Semester
                );

            // Assert
            Assert.IsNotNull(joursRemplaces);
        }

        [TestMethod]
        public async Task TestCourses()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var courses = await service.Courses();

            // Assert
            Assert.IsNotNull(courses);
        }

        [TestMethod]
        public async Task TestEvaluations()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var evaluations = await service.Evaluations(
                TestSettings.Course,
                TestSettings.Group,
                TestSettings.Semester           
                );

            // Assert
            Assert.IsNotNull(evaluations);
        }

        [TestMethod]
        public async Task TestScheduleAndTeachers()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var activiteesEnseignants = await service.ScheduleAndTeachers(
                TestSettings.Semester                
                );

            // Assert
            Assert.IsNotNull(activiteesEnseignants);
        }

        [TestMethod]
        public async Task TestPrograms()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var programs = await service.Programs();

            // Assert
            Assert.IsNotNull(programs);
        }

        [TestMethod]
        public async Task TestSemesters()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var semesters = await service.Semesters();

            // Assert
            Assert.IsNotNull(semesters);
        }
    }
}
