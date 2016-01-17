using Akavache;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Business.Extensions;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Client.Extensions.Signets;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Implementations.Signets;
using Ets.Mobile.Entities.Auth;
using Ets.Mobile.Entities.Signets;
using Security.Algorithms;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Services
{
    public class SignetsService : ISignetsService
    {
        public SignetsService(ISignetsBusinessService signetsService, SignetsAbstractFactory factory)
        {
            _signetsService = signetsService;
            _factory = factory;
        }

        public SignetsService(ISignetsBusinessService signetsService, SignetsAbstractFactory factory, EtsUserCredentials account)
        {
            _signetsService = signetsService;
            _factory = factory;
            _userCredentials = account;
        }

        public void SetCredentials(EtsUserCredentials vm)
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
            
            this.HandleError(userDetailsResult);

            var userDetailsVm = _factory.CreateFor<UserDetailsResult, UserDetailsVm>(userDetailsResult);

            userDetailsVm.Username = _userCredentials.Username;

            userDetailsVm.Image = await BlobCache.UserAccount.LoadImageFromUrl("gravatar",
                "http://www.gravatar.com/avatar/" +
                $"{Md5Hash.GetHashString(userDetailsVm.Email.ToLower())}", true).ToTask();

            return userDetailsVm;
        }

        public async Task<CourseVm[]> Courses()
        {
            var coursesResult = await _signetsService.Courses(_userCredentials.Username, _userCredentials.Password);

            this.HandleError(coursesResult);

            return _factory.CreateFor<CoursesResult, List<CourseVm>>(coursesResult).ToArray();
        }

        public async Task<CourseIntervalVm[]> CoursesIntervalSemester(string startSemester, string endSemester)
        {
            var coursesIntervalResult = await _signetsService.CoursesIntervalSemester(_userCredentials.Username, _userCredentials.Password, startSemester, endSemester);

            this.HandleError(coursesIntervalResult);

            return _factory.CreateFor<CoursesIntervalSemesterResult, List<CourseIntervalVm>>(coursesIntervalResult).ToArray();
        }

        public async Task<SemesterVm[]> Semesters()
        {
            var semestersResult = await _signetsService.Semesters(_userCredentials.Username, _userCredentials.Password);

            this.HandleError(semestersResult);

            return _factory.CreateFor<SemestersResult, List<SemesterVm>>(semestersResult).ToArray();
        }

        public async Task<ProgramVm[]> Programs()
        {
            var programsResult = await _signetsService.Programs(_userCredentials.Username, _userCredentials.Password);

            this.HandleError(programsResult);

            return _factory.CreateFor<ProgramsResult, List<ProgramVm>>(programsResult).ToArray();
        }

        public async Task<TeammateVm[]> Teammates(string courseAbridgedName, string group, string semesterAbridgedName, string evaluationElementName = "")
        {
            var teammatesResult = await _signetsService.Teammates(_userCredentials.Username, _userCredentials.Password, courseAbridgedName, group, semesterAbridgedName, evaluationElementName);

            this.HandleError(teammatesResult);

            return _factory.CreateFor<TeammatesResult, List<TeammateVm>>(teammatesResult).ToArray();
        }

        public async Task<EvaluationsVm> Evaluations(string courseAbridgedName, string group, string semesterAbridgedName)
        {
            if (semesterAbridgedName == "s.o." || semesterAbridgedName == "N/A") // s.o. are not recognized by the webservice
            {
                return new EvaluationsVm();
            }

            var evaluationsVm = await _signetsService.Evaluations(_userCredentials.Username, _userCredentials.Password,
                courseAbridgedName, group, semesterAbridgedName);

            this.HandleError(evaluationsVm);

            return _factory.CreateFor<EvaluationsResult, EvaluationsVm>(evaluationsVm);
        }

        public async Task<ScheduleAndTeachersVm> ScheduleAndTeachers(string semesterAbridgedName)
        {
            var scheduleAndTeachersResult = await _signetsService.ScheduleAndTeachers(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName);

            this.HandleError(scheduleAndTeachersResult);

            return _factory.CreateFor<ScheduleAndTeachersResult, ScheduleAndTeachersVm>(scheduleAndTeachersResult);
        }

        public async Task<ScheduleFinalExamVm[]> ScheduleFinalExams(string semesterAbridgedName)
        {
            var scheduleFinalExamsResult = await _signetsService.ScheduleFinalExams(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName);

            this.HandleError(scheduleFinalExamsResult);

            return _factory.CreateFor<ScheduleFinalExamsResult, List<ScheduleFinalExamVm>>(scheduleFinalExamsResult).ToArray();
        }

        public async Task<CourseForSemesterVm[]> CoursesForSemester(string semesterAbridgedName, string courseAbridgedName)
        {
            var coursesForSemesterResult = await _signetsService.CoursesForSemester(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName, courseAbridgedName);

            this.HandleError(coursesForSemesterResult);

            return _factory.CreateFor<CourseForSemesterResult, List<CourseForSemesterVm>>(coursesForSemesterResult).ToArray();
        }

        public async Task<ReplacedDayVm[]> ReplacedDays(string semesterAbridgedName)
        {
            var replacedDaysResult = await _signetsService.ReplacedDays(semesterAbridgedName);

            this.HandleError(replacedDaysResult);

            return _factory.CreateFor<ReplacedDaysResult, List<ReplacedDayVm>>(replacedDaysResult).ToArray();
        }

        public async Task<ScheduleVm[]> Schedule(string semesterAbridgedName, string courseAbridgedNameAndGroup = "", string startDate = "", string endDate = "")
        {
            var scheduleResult = await _signetsService.Schedule(_userCredentials.Username, _userCredentials.Password, semesterAbridgedName, courseAbridgedNameAndGroup, startDate, endDate);

            this.HandleError(scheduleResult);

            return _factory.CreateFor<ScheduleResult, List<ScheduleVm>>(scheduleResult)
                .Select(s => 
                {
                    // Embed Semester
                    s.Semester = semesterAbridgedName;
                    return s;
                })
                .ToArray();
        }

        #region Properties

        private readonly ISignetsBusinessService _signetsService;
        private EtsUserCredentials _userCredentials;
        private readonly SignetsAbstractFactory _factory;

        #endregion
    }
}