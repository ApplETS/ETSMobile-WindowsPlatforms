using Ets.Mobile.Entities.Moodle;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Contracts
{
    public interface IMoodleService : ISetCredentials
    {
        Task<MoodleSiteInfoVm> SiteInfo(string token = "", bool loadUserPicture = false);
        Task<MoodleCourseVm[]> Courses();
        Task<MoodleCourseContentVm[]> CoursesContents(int courseId);
    }
}