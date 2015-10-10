using System.Threading.Tasks;
using Ets.Mobile.Client.Contracts;

namespace Ets.Mobile.Client.Shared.Tests.Contracts
{
    public interface ISignetsServiceTest
    {
        ISignetsService GetSignetsServices();
        Task TestCourses();
        Task TestEvaluations();
        Task TestGetMyProfile();
        Task TestLogin();
        Task TestPrograms();
        Task TestReplacedDays();
        Task TestSchedule();
        Task TestScheduleAndTeachers();
        Task TestSemesters();
    }
}
