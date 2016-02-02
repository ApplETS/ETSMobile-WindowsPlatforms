using Ets.Mobile.Client.Contracts;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Tests.Contracts
{
    public interface ISignetsServiceTest
    {
        Task LoginTest();
        Task UserDetailsTest();
        Task CoursesTest();
        Task CoursesIntervalSemesterTest();
        Task SemestersTest();
        Task ProgramsTest();
        Task TeammatesTest();
        Task EvaluationsTest();
        Task ScheduleAndTeachersTest();
        Task ScheduleFinalExamsTest();
        Task CoursesForSemesterTest();
        Task ReplacedDaysTest();
        Task ScheduleTest();
    }
}