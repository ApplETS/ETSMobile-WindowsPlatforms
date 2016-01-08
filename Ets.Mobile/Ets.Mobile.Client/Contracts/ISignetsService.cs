using Ets.Mobile.Entities.Signets;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Contracts
{
    public interface ISignetsService : ISetCredentials
    {
        Task<bool> Login(string userName, string password);

        Task<UserDetailsVm> UserDetails();

        Task<CourseVm[]> Courses();

        Task<CourseIntervalVm[]> CoursesIntervalSemester(string startSemester, string endSemester);

        Task<SemesterVm[]> Semesters();

        Task<ProgramVm[]> Programs();

        Task<TeammateVm[]> Teammates(string courseAbridgedName, string group, string semesterAbridgedName, string evaluationElementName = "");

        Task<EvaluationsVm> Evaluations(string courseAbridgedName, string group, string semesterAbridgedName);

        Task<ScheduleAndTeachersVm> ScheduleAndTeachers(string semesterAbridgedName);

        Task<ScheduleFinalExamVm[]> ScheduleFinalExams(string semesterAbridgedName);

        Task<CourseForSemesterVm[]> CoursesForSemester(string semesterAbridgedName, string courseAbridgedName);

        Task<ReplacedDayVm[]> ReplacedDays(string semesterAbridgedName);

        Task<ScheduleVm[]> Schedule(string semesterAbridgedName, string courseAbridgedNameAndGroup = "", string startDate = "", string endDate = "");
    }
}