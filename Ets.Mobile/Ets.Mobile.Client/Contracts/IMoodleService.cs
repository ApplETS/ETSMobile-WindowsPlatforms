using Ets.Mobile.Entities.Moodle;
using System.Threading.Tasks;
using Ets.Mobile.Business.Entities.Moodle.Token;

namespace Ets.Mobile.Client.Contracts
{
    public interface IMoodleService : ISetCredentials
    {
        Task<string> GetToken();
        Task<MoodleSiteInfoVm> SiteInfo(string token = "", bool loadUserPicture = false);
        Task<MoodleCourseVm[]> Courses();
        Task<MoodleCourseContentVm[]> CoursesContents(int courseId);
    }
}