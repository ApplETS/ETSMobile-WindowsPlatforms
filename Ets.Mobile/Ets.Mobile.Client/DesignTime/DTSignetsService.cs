using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Client.Contracts;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.DesignTime
{
    public class DtSignetsService : ISignetsService
    {
        private readonly ISignetsBusinessService _businessService;
        private readonly SignetsAccountVm _vm;

        public DtSignetsService(ISignetsBusinessService businessService)
        {
            _businessService = businessService;
        }

        public void SetCredentials(SignetsAccountVm vm)
        {
        }

        public async Task<bool> Login(string userName, string password)
        {
            var loginResult = await _businessService.Login("", "");

            return loginResult.IsAuthentificated;
        }

        public Task<UserDetailsVm> UserDetails()
        {
            var coursesResult = new UserDetailsVm
            {
                Balance = "0",
                FirstName = "First",
                LastName = "Last",
                PermanentCode = "LASF0992022"
            };

            return ReturnT(coursesResult);
        }

        public Task<List<CourseVm>> Courses()
        {
            var coursesResult = new List<CourseVm>()
            {
                new CourseVm()
            };

            return ReturnT(coursesResult);
        }

        public Task<List<CourseIntervalVm>> CoursesIntervalSemester(string startSemester, string endSemester)
        {
            var coursesIntervalResult = new List<CourseIntervalVm>()
            {
                new CourseIntervalVm()
            };

            return ReturnT(coursesIntervalResult);
        }

        public Task<List<SemesterVm>> Semesters()
        {
            var semestersResult = new List<SemesterVm>
            {
                new SemesterVm
                {
                    AbridgedName = "E2015",
                    Name = "Été 2015",
                    StartDate = new DateTime(2015, 04, 27),
                    EndDate = new DateTime(2015, 08, 08),
                    StartCancellationDateWithReimbursement = new DateTime(2015, 04, 27),
                    EndCancellationDateWithReimbursement = new DateTime(2015, 05, 08),
                    StartCancellationDateWithReimbursementForNewStudent = new DateTime(2015, 05, 01),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2015, 05, 08),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015, 05, 19),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015, 07, 03)
                }
            };

            return ReturnT(semestersResult);
        }

        public Task<List<ProgramVm>> Programs()
        {
            var programsResult = new List<ProgramVm>
            {
                new ProgramVm
                {
                    Code = "0725",
                    Name = "Microprogramme de 1er cycle en enseignement coopératif I",
                    Average = 60,
                    CompletedCreditsCount = "26",
                    RegisteredCreditsCount = "4",
                    PotentialCreditsCount = "56",
                    SearchCreditsCount = "35",
                    FailedCreditsCount = "0",
                    SuceededCreditsCount = "4",
                    EquivalenceCount = "0",
                    SemesterStart = "A2014",
                    SemesterEnd = "A2014",
                    Status = "diplômé"
                }
            };

            return ReturnT(programsResult);
        }

        public Task<List<TeammateVm>> Teammates(string courseAbridgedName, string group, string semesterAbridgedName, string evaluationElementName = "")
        {
            var teammatesResult = new List<TeammateVm>()
            {
                new TeammateVm
                {
                    FirstName = "Lorem",
                    LastName = "Ipsum",
                    Email = "lorem.ipsum@ens.etsmtl.ca"
                }
            };

            return ReturnT(teammatesResult);
        }

        public Task<EvaluationsVm> Evaluations(string courseAbridgedName, string group, string semesterAbridgedName)
        {
            var evaluationsResult = new EvaluationsVm
            {
                StandardDeviationOfClass = 95,
                Evaluations = null,
                MedianOfClass = 75,
                AverageOfClass = 79,
                ActualGrade = 95,
                ActualGradeOfIndividualElements = 95,
                GradeOnHundredOfIndividualElements = 95,
                PercentileOfClass = 75,
                FinalGradeOnHundred = 95
            };

            return ReturnT(evaluationsResult);
        }

        public Task<ScheduleAndTeachersVm> ScheduleAndTeachers(string semesterAbridgedName)
        {
            var scheduleAndTeachersResult = new ScheduleAndTeachersVm
            {
                Activities = new List<ActivityVm>(),
                Teachers = new List<TeacherVm>()
            };

            return ReturnT(scheduleAndTeachersResult);
        }

        public Task<List<ScheduleFinalExamVm>> ScheduleFinalExams(string semesterAbridgedName)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseForSemesterVm>> CoursesForSemester(string semesterAbridgedName, string courseAbridgedName)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReplacedDayVm>> ReplacedDays(string semesterAbridgedName)
        {
            throw new NotImplementedException();
        }

        public Task<List<ScheduleVm>> Schedule(string semesterAbridgedName, string courseAbridgedNameAndGroup = "", string startDate = "", string endDate = "")
        {
            throw new NotImplementedException();
        }

        public Task<T> ReturnT<T>(T result)
        {
            return Task.FromResult(result);
        }
    }
}
