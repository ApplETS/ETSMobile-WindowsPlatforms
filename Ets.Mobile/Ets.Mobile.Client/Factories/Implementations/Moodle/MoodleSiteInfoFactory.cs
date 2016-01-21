using Ets.Mobile.Business.Entities.Moodle.SiteInfo;
using Ets.Mobile.Client.Factories.Interfaces.Moodle;
using Ets.Mobile.Entities.Moodle;

namespace Ets.Mobile.Client.Factories.Implementations.Moodle
{
    public class MoodleSiteInfoFactory : IMoodleSiteInfoFactory
    {
        public MoodleSiteInfoVm Create(MoodleSiteInfo result)
        {
            return new MoodleSiteInfoVm
            {
                SiteName = result.SiteName,
                Username = result.Username,
                FirstName = result.FirstName,
                LastName = result.LastName,
                FullName = result.FullName,
                UserId = result.UserId,
                SiteUrl = result.SiteUrl,
                UserPictureUrl = result.UserPictureUrl
            };
        }
    }
}