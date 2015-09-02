using System.Collections.Generic;
using System.Threading.Tasks;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Contracts
{
    public interface ISignetsService
    {
        void SetCredentials(SignetsAccountVm vm);

        Task<bool> Login(string userName, string password);

        Task<UserDetailsVm> UserDetails();

        Task<List<CourseVm>> Courses();

        Task<List<CourseIntervalVm>> CoursesIntervalSemester(string startSemester, string endSemester);

        Task<List<SemesterVm>> Semesters();

        Task<List<ProgramVm>> Programs();

        Task<List<TeammateVm>> Teammates(string courseAbridgedName, string group, string semesterAbridgedName, string evaluationElementName = "");

        Task<EvaluationsVm> Evaluations(string courseAbridgedName, string group, string semesterAbridgedName);

        Task<ScheduleAndTeachersVm> ScheduleAndTeachers(string semesterAbridgedName);

        Task<List<ScheduleFinalExamVm>> ScheduleFinalExams(string semesterAbridgedName);

        Task<List<CourseForSemesterVm>> CoursesForSemester(string semesterAbridgedName, string courseAbridgedName);

        Task<List<ReplacedDayVm>> ReplacedDays(string semesterAbridgedName);

        Task<List<ScheduleVm>> Schedule(string semesterAbridgedName, string courseAbridgedNameAndGroup = "", string startDate = "", string endDate = "");
    }
}
