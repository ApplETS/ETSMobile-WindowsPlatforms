using Ets.Mobile.Business.Entities.Moodle.Courses;
using Ets.Mobile.Business.Entities.Moodle.CoursesContent;
using Ets.Mobile.Business.Entities.Moodle.SiteInfo;
using Ets.Mobile.Business.Entities.Moodle.Token;
using Refit;
using System.Threading.Tasks;

namespace Ets.Mobile.Business.Contracts
{
    public interface IMoodleBusinessService
    {
        [Post("/login/token.php?username={username}&password={password}&service=moodle_mobile_app")]
        Task<MoodleToken> Token(string username, string password);

        [Post("/webservice/rest/server.php?moodlewsrestformat=json&wstoken={token}&wsfunction=moodle_webservice_get_siteinfo")]
        Task<MoodleSiteInfo> SiteInfo(string token);

        [Post("/webservice/rest/server.php?moodlewsrestformat=json&wstoken={token}&wsfunction=moodle_enrol_get_users_courses&userid={userId}")]
        Task<MoodleCourse[]> Courses(string token, int userId);

        [Post("/webservice/rest/server.php?moodlewsrestformat=json&wstoken={token}&wsfunction=core_course_get_contents&courseid={courseId}")]
        Task<MoodleCourseContent[]> CoursesContents(string token, int courseId);
    }
}