using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;
using Ets.Mobile.Business.Shared.Tests.Contracts;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Shared.Tests;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ModernHttpClient;
using Newtonsoft.Json;
using Refit;
using Splat;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ets.Mobile.Business.Shared.Tests.Services
{
    [TestClass]
	public class SignetsBusinessServiceTest : MockBase, ISignetsBusinessServiceTest
	{
		public Task<ISignetsBusinessService> GetSignetsServices()
		{
			if (locator.GetService<ISignetsBusinessService>() == null)
			{
                locator.RegisterLazySingleton(() => new EtsUserCredentials(TestSettings.Username, TestSettings.Password), typeof(ICredentials));
                locator.RegisterLazySingleton(() =>
                    new SignetsClientInfo { Url = "https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx" },
                    typeof(SignetsClientInfo)
                );

                // NetCache.UserInitiated
                var client = new HttpClient(new NativeMessageHandler())
                {
                    BaseAddress = new Uri(locator.GetService<SignetsClientInfo>().Url),
                };

                var refitSettings = new RefitSettings
                {
                    JsonSerializerSettings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new DecimalConverter(), new DoubleConverter() }
                    }
                };

                locator.RegisterLazySingleton(() => RestService.For<ISignetsBusinessService>(client, refitSettings), typeof(ISignetsBusinessService));
            }

			return Task.FromResult(locator.GetService<ISignetsBusinessService>());
		}

		[TestMethod]
		public async Task TestDonneesAuthentificationValides()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var valid = await service.Login(
				TestSettings.Username, 
				TestSettings.Password
				);

			// Assert
			Assert.IsTrue(valid.IsAuthentificated);
		}

		[TestMethod]
		public async Task TestInfoEtudiant()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var valid = await service.UserDetails(TestSettings.Username, TestSettings.Password);

			// Assert
			Assert.IsNotNull(valid);
		}

		[TestMethod]
		public async Task TestLireHoraireDesSeances()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var sceances = await service.Schedule(
				TestSettings.CourseGroup,
				TestSettings.DateDebut,
				TestSettings.DateFin,
				TestSettings.Semester
				);

			// Assert
			Assert.IsNotNull(sceances);
		}

		[TestMethod]
		public async Task TestListeCours()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var sceances = await service.Courses(TestSettings.Username, TestSettings.Password);

			// Assert
            Assert.IsTrue(sceances != null);
			Assert.IsTrue(sceances.Courses.Count > 0);
		}

		[TestMethod]
		public async Task TestListeProgrammes()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var programs = await service.Programs(TestSettings.Username, TestSettings.Password);

			// Assert
			Assert.IsNotNull(programs);
		}

		[TestMethod]
		public async Task TestListeSessions()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var programs = await service.Semesters(TestSettings.Username, TestSettings.Password);

			// Assert
			Assert.IsNotNull(programs);
		}

		[TestMethod]
		public async Task TestListeElementsEvaluation()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var evaluations = await service.Evaluations(
                TestSettings.Username,
                TestSettings.Password,
				TestSettings.Course,
				TestSettings.Group,
				TestSettings.Semester
				);

			// Assert
			Assert.IsNotNull(evaluations);
		}

		[TestMethod]
		public async Task TestLireJoursRemplaces()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var jourRemplace = await service.ReplacedDays(
				TestSettings.Semester
				);

			// Assert
			Assert.IsNotNull(jourRemplace);
		}

		[TestMethod]
		public async Task TestListeHoraireEtProf()
		{
			// Arrange
			var service = await GetSignetsServices();

			// Act
			var horaireEtProf = await service.ScheduleAndTeachers(
                TestSettings.Username,
                TestSettings.Password,
				TestSettings.Semester
				);

			// Assert
			Assert.IsNotNull(horaireEtProf);
		}
	}
}