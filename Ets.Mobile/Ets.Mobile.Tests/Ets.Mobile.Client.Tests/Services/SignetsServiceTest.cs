using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Mocks.Fixtures;
using Ets.Mobile.Business.Mocks.Services;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations.Signets;
using Ets.Mobile.Client.Services;
using Ets.Mobile.Client.Tests.Contracts;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Shared.Tests;
using Kent.Boogaart.PCLMock;
using Splat;
using System.Threading.Tasks;
using Xunit;

namespace Ets.Mobile.Client.Tests.Services
{
    public class SignetsServiceTest : MockBase, ISignetsServiceTest
    {
        public ISignetsService GetSignetsServices()
        {
            if(locator.GetService<ISignetsService>() == null)
            {
                locator.RegisterLazySingleton(() => new SignetsBusinessServiceMock(MockBehavior.Strict), typeof(ISignetsBusinessService));
                locator.RegisterLazySingleton(() => new SignetsFactory(), typeof(SignetsAbstractFactory));
                locator.RegisterLazySingleton(() => new SignetsService(locator.GetService<ISignetsBusinessService>(), locator.GetService<SignetsAbstractFactory>(), new EtsUserCredentials("AK12345", "myPass")), typeof(ISignetsService));
            }
            return locator.GetService<ISignetsService>();
        }
        
        [Fact(DisplayName="Signets/Login")]
        public async Task LoginTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var isLoggedIn = await service.Login(
                "ak12345",
                "myPass"
            );

            // Assert
            Assert.True(isLoggedIn);
        }

        [Fact(DisplayName="Signets/UserDetails")]
        public async Task UserDetailsTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var userDetails = await service.UserDetails();

            // Assert
            Assert.StrictEqual(SignetsBusinessFixtures.UserDetails.Balance, userDetails.Balance);
            Assert.StrictEqual(SignetsBusinessFixtures.UserDetails.FirstName, userDetails.FirstName);
            Assert.StrictEqual(SignetsBusinessFixtures.UserDetails.LastName, userDetails.LastName);
            Assert.StrictEqual(SignetsBusinessFixtures.UserDetails.PermanentCode, userDetails.PermanentCode);
            Assert.StrictEqual(SignetsBusinessFixtures.UserDetails.IsMan, userDetails.IsMan);
        }

        [Fact(DisplayName="Signets/Schedule")]
        public async Task ScheduleTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var schedule = await service.Schedule(
                "H2016",
                "ABC123-01"
            );

            // Assert
            Assert.NotEmpty(schedule);
            Assert.StrictEqual(SignetsBusinessFixtures.Schedule.Schedules.Count, schedule.Length);
        }

        [Fact(DisplayName="Signets/CoursesForSemester")]
        public async Task CoursesForSemesterTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var coursesForSemester = await service.CoursesForSemester(
                "H2016", 
                "ABC123"
            );

            // Assert
            Assert.NotEmpty(coursesForSemester);
            Assert.StrictEqual(SignetsBusinessFixtures.CoursesForSemester.CoursesForSemester.Count, coursesForSemester.Length);
        }

        [Fact(DisplayName="Signets/ReplacedDays")]
        public async Task ReplacedDaysTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var replacedDays = await service.ReplacedDays(
                "H2016"
            );

            // Assert
            Assert.NotEmpty(replacedDays);
            Assert.StrictEqual(SignetsBusinessFixtures.ReplacedDays.ReplacedDays.Count, replacedDays.Length);
        }

        [Fact(DisplayName="Signets/Courses")]
        public async Task CoursesTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var courses = await service.Courses();

            // Assert
            Assert.NotEmpty(courses);
            Assert.StrictEqual(SignetsBusinessFixtures.Courses.Courses.Count, courses.Length);
        }

        [Fact(DisplayName="Signets/CoursesIntervalSemester")]
        public async Task CoursesIntervalSemesterTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var coursesIntervalSemestercourses = await service.CoursesIntervalSemester(
                "A2015", 
                "H2016"
            );

            // Assert
            Assert.NotEmpty(coursesIntervalSemestercourses);
            Assert.StrictEqual(SignetsBusinessFixtures.CoursesInterval.Courses.Count, coursesIntervalSemestercourses.Length);
        }

        [Fact(DisplayName="Signets/Teammates")]
        public async Task TeammatesTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var teammates = await service.Teammates(
                "ABC123", 
                "01", 
                "H2016", 
                string.Empty
            );

            // Assert
            Assert.NotEmpty(teammates);
            Assert.StrictEqual(SignetsBusinessFixtures.Teammates.Teammates.Count, teammates.Length);
        }

        [Fact(DisplayName="Signets/Evaluations")]
        public async Task EvaluationsTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var evaluations = await service.Evaluations(
                "ABC123",
                "01",
                "H2016"        
            );

            // Assert
            Assert.NotNull(evaluations);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.ActualGrade, evaluations.ActualGrade);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.ActualGradeOfIndividualElements, evaluations.ActualGradeOfIndividualElements);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Average, evaluations.Average);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.FinalGradeOnHundred, evaluations.FinalGradeOnHundred);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.GradeOnHundredOfIndividualElements, evaluations.GradeOnHundredOfIndividualElements);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Median, evaluations.Median);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Percentile, evaluations.Percentile);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.StandardDeviation, evaluations.StandardDeviation);
            Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations.Count, evaluations.Evaluations.Count);
            var indexOfInnerEvaluation = 0;
            Assert.All(evaluations.Evaluations, eval =>
            {
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Average, eval.Average);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].CourseAndGroup, eval.CourseAndGroup);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Grade, eval.Grade);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].IgnoredFromCalculation == "Oui", eval.IgnoredFromCalculation);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Median, eval.Median);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].MessageOfTeacher, eval.MessageOfTeacher);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Name, eval.Name);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Percentile, eval.Percentile);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Published, eval.Published);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].StandardDeviation, eval.StandardDeviation);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].TargetDate, eval.TargetDate);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Team, eval.Team);
                Assert.StrictEqual(double.Parse(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Total), eval.Total);
                Assert.StrictEqual(SignetsBusinessFixtures.Evaluations.Evaluations[indexOfInnerEvaluation].Weighting, eval.Weighting);
                indexOfInnerEvaluation++;
            });
        }

        [Fact(DisplayName="Signets/ScheduleAndTeachers")]
        public async Task ScheduleAndTeachersTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var scheduleAndTeacher = await service.ScheduleAndTeachers(
                "H2016"
            );

            // Assert
            Assert.NotNull(scheduleAndTeacher);
            Assert.NotEmpty(scheduleAndTeacher.Activities);
            Assert.NotEmpty(scheduleAndTeacher.Teachers);
            Assert.StrictEqual(SignetsBusinessFixtures.ScheduleAndTeachers.Activities.Count, scheduleAndTeacher.Activities.Length);
            Assert.StrictEqual(SignetsBusinessFixtures.ScheduleAndTeachers.Teachers.Count, scheduleAndTeacher.Teachers.Length);
        }

        [Fact(DisplayName="Signets/ScheduleFinalExams")]
        public async Task ScheduleFinalExamsTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var scheduleFinalExamsscheduleAndTeacher = await service.ScheduleFinalExams(
                "H2016"
            );

            // Assert
            Assert.NotEmpty(scheduleFinalExamsscheduleAndTeacher);
            Assert.StrictEqual(SignetsBusinessFixtures.ScheduleFinalExams.ScheduleFinalExams.Count, scheduleFinalExamsscheduleAndTeacher.Length);
        }

        [Fact(DisplayName="Signets/Programs")]
        public async Task ProgramsTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var programs = await service.Programs();

            // Assert
            Assert.NotEmpty(programs);
            Assert.StrictEqual(SignetsBusinessFixtures.Programs.Programs.Count, programs.Length);
        }

        [Fact(DisplayName="Signets/Semesters")]
        public async Task SemestersTest()
        {
            // Arrange
            var service = GetSignetsServices();

            // Act
            var semesters = await service.Semesters();

            // Assert
            Assert.NotEmpty(semesters);
            Assert.StrictEqual(SignetsBusinessFixtures.Semesters.Semesters.Count, semesters.Length);
        }
    }
}