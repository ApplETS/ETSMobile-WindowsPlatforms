using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Akavache;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Extensions.Signets;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations;
using Ets.Mobile.Entities.Signets;
using StoreFramework.Security;

namespace Ets.Mobile.Client.Services
{
    public class SignetsService : ISignetsService
    {
        private readonly ISignetsBusinessService _signetsService;
        private SignetsAccountVm _userCredentials;
        private readonly SignetsAbstractFactory _factory;

        public SignetsService(ISignetsBusinessService signetsService, SignetsAbstractFactory factory)
        {
            _signetsService = signetsService;
            _factory = factory;
        }

        public void SetCredentials(SignetsAccountVm vm)
        {
            _userCredentials = vm;
        }

        public async Task<bool> Login(string userName, string password)
        {
            var loginResult = await _signetsService.Login(userName, password);

            return loginResult.IsAuthentificated;
        }

        public async Task<UserDetailsVm> UserDetails()
        {
            var userDetailsResult = await _signetsService.UserDetails(_userCredentials.Username, _userCredentials.Password);
            
            _signetsService.HandleError(userDetailsResult);

            var userDetailsVm = _factory.CreateFor<UserDetailsResult, UserDetailsVm>(userDetailsResult);
            
            userDetailsVm.Image = await BlobCache.UserAccount.LoadImageFromUrl("gravatar",
                $"http://www.gravatar.com/avatar/{Md5Provider.GetHashString($"{_userCredentials.Username}@ens.etsmtl.ca".ToLower())}", true).ToTask();

            return userDetailsVm;
        }

        public async Task<List<CourseVm>> Courses()
        {
            var coursesResult = await _signetsService.Courses(_userCredentials.Username, _userCredentials.Password);

            _signetsService.HandleError(coursesResult);

            return _factory.CreateFor<CoursesResult, List<CourseVm>>(coursesResult);
        }

        public async Task<List<CourseIntervalVm>> CoursesIntervalSemester(string startSemester, string endSemester)
        {
            var coursesIntervalResult = await _signetsService.CoursesIntervalSemester(_userCredentials.Username, _userCredentials.Password, startSemester, endSemester);

            _signetsService.HandleError(coursesIntervalResult);

            return _factory.CreateFor<CoursesIntervalSemesterResult, List<CourseIntervalVm>>(coursesIntervalResult);
        }

        public async Task<List<SemesterVm>> Semesters()
        {
            var semestersResult = await _signetsService.Semesters(_userCredentials.Username, _userCredentials.Password);

            _signetsService.HandleError(semestersResult);

            return _factory.CreateFor<SemestersResult, List<SemesterVm>>(semestersResult);
        }

        public async Task<List<ProgramVm>> Programs()
        {
            var programsResult = await _signetsService.Programs(_userCredentials.Username, _userCredentials.Password);

            _signetsService.HandleError(programsResult);

            return _factory.CreateFor<ProgramsResult, List<ProgramVm>>(programsResult);
        }

        public async Task<List<TeammateVm>> Teammates(string courseAbridgedName, string group, string semesterAbridgedName, string evaluationElementName = "")
        {
            var teammatesResult = await _signetsService.Teammates(_userCredentials.Username, _userCredentials.Password, courseAbridgedName, group, semesterAbridgedName, evaluationElementName);

            _signetsService.HandleError(teammatesResult);

            return _factory.CreateFor<TeammatesResult, List<TeammateVm>>(teammatesResult);
        }

        public async Task<EvaluationsVm> Evaluations(string courseAbridgedName, string group, string semesterAbridgedName)
        {
            if (semesterAbridgedName == "s.o.")
            {
                return new EvaluationsVm();
            }

            var evaluationsVm = await _signetsService.Evaluations(_userCredentials.Username, _userCredentials.Password,
                courseAbridgedName, group, semesterAbridgedName);

            _signetsService.HandleError(evaluationsVm);

            return _factory.CreateFor<EvaluationsResult, EvaluationsVm>(evaluationsVm);
        }

        public async Task<ScheduleAndTeachersVm> ScheduleAndTeachers(string semesterAbridgedName)
        {
            var scheduleAndTeachersResult = await _signetsService.ScheduleAndTeachers(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName);
            
            _signetsService.HandleError(scheduleAndTeachersResult);

            return _factory.CreateFor<ScheduleAndTeachersResult, ScheduleAndTeachersVm>(scheduleAndTeachersResult);
        }

        public async Task<List<ScheduleFinalExamVm>> ScheduleFinalExams(string semesterAbridgedName)
        {
            var scheduleFinalExamsResult = await _signetsService.ScheduleFinalExams(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName);

            _signetsService.HandleError(scheduleFinalExamsResult);

            return _factory.CreateFor<ScheduleFinalExamsResult, List<ScheduleFinalExamVm>>(scheduleFinalExamsResult);
        }

        public async Task<List<CourseForSemesterVm>> CoursesForSemester(string semesterAbridgedName, string courseAbridgedName)
        {
            var coursesForSemesterResult = await _signetsService.CoursesForSemester(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName, courseAbridgedName);

            _signetsService.HandleError(coursesForSemesterResult);

            return _factory.CreateFor<CourseForSemesterResult, List<CourseForSemesterVm>>(coursesForSemesterResult);
        }

        public async Task<List<ReplacedDayVm>> ReplacedDays(string semesterAbridgedName)
        {
            var replacedDaysResult = await _signetsService.ReplacedDays(semesterAbridgedName);

            _signetsService.HandleError(replacedDaysResult);

            return _factory.CreateFor<ReplacedDaysResult, List<ReplacedDayVm>>(replacedDaysResult);
        }

        public async Task<List<ScheduleVm>> Schedule(string semesterAbridgedName, string courseAbridgedNameAndGroup = "", string startDate = "", string endDate = "")
        {
            var scheduleResult = await _signetsService.Schedule(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName, courseAbridgedNameAndGroup, startDate, endDate);

            _signetsService.HandleError(scheduleResult);

            return _factory.CreateFor<ScheduleResult, List<ScheduleVm>>(scheduleResult);
        }
    }
}
