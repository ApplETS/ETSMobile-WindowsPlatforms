#if WINDOWS_PHONE_APP
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.DesignTime;
using Ets.Mobile.Business.Shared.Tests.Contracts;
using Ets.Mobile.Entities.ServiceInfo;
using Ets.Mobile.Entities.Signets;
using Ets.Mobile.Shared.Tests;
using Fusillade;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Refit;
using Splat;

#endif

namespace Ets.Mobile.Business.Shared.Tests.Services
{
#if WINDOWS_PHONE_APP
    [TestClass]
	public class SignetsBusinessServiceTest : DTBase, ISignetsBusinessServiceTest
	{
		public Task<ISignetsBusinessService> GetSignetsServices()
		{

			if (locator.GetService<ISignetsBusinessService>() != null)
			{
				if (UseDT)
				{
                    locator.Register(() => new DtSignetsBusinessService(), typeof(ISignetsBusinessService));
				}
				else
				{
                    locator.Register(() => new SignetsAccountVm(TestSettings.Username, TestSettings.Password), typeof(ICredentials));
                    locator.Register(() =>
                        new SignetsServiceInfo { Url = "https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx" }, 
                        typeof(IClientInfo)
                    );
                    var client = new HttpClient(NetCache.UserInitiated)
                    {
                        BaseAddress = new Uri("https://signets-ens.etsmtl.ca/Secure/WebServices/SignetsMobile.asmx"),
                    };
                    locator.Register(() => RestService.For<ISignetsBusinessService>(client), typeof(ISignetsBusinessService));
				}                
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
			Assert.IsNotNull(sceances);
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
#endif
}