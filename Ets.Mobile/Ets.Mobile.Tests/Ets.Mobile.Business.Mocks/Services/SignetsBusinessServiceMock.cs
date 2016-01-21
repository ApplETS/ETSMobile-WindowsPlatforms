using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Business.Mocks.Fixtures;
using Kent.Boogaart.PCLMock;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ets.Mobile.Business.Mocks.Services
{
    public class SignetsBusinessServiceMock : MockBase<ISignetsBusinessService>, ISignetsBusinessService
    {
        public SignetsBusinessServiceMock(MockBehavior behavior) : base(behavior)
        {
        }

        public Task<LoginResult> LoginRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Login);
        }

        public Task<UserDetailsResult> UserDetailsRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.UserDetails);
        }

        public Task<CoursesResult> CoursesRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Courses);
        }

        public Task<CoursesIntervalSemesterResult> CoursesIntervalSemesterRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.CoursesInterval);
        }

        public Task<SemestersResult> SemestersRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Semesters);
        }

        public Task<ProgramsResult> ProgramsRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Programs);
        }

        public Task<TeammatesResult> TeammatesRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Teammates);
        }

        public Task<EvaluationsResult> EvaluationsRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Evaluations);
        }

        public Task<ScheduleAndTeachersResult> ScheduleAndTeachersRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.ScheduleAndTeachers);
        }

        public Task<ScheduleFinalExamsResult> ScheduleFinalExamsRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.ScheduleFinalExams);
        }

        public Task<CourseForSemesterResult> CoursesForSemesterRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.CoursesForSemester);
        }

        public Task<ReplacedDaysResult> ReplacedDaysRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.ReplacedDays);
        }

        public Task<ScheduleResult> ScheduleRaw(Dictionary<string, string> form)
        {
            return Task.FromResult(SignetsBusinessFixtures.Schedule);
        }
    }
}