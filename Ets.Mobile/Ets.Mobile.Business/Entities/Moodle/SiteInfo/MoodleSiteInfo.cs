using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Moodle.SiteInfo
{
    public class MoodleSiteInfo
    {
        [JsonProperty("sitename")]
        public string SiteName { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        [JsonProperty("userid")]
        public int UserId { get; set; }
        [JsonProperty("siteurl")]
        public string SiteUrl { get; set; }
        [JsonProperty("userpictureurl")]
        public string UserPictureUrl { get; set; }
        [JsonProperty("functions")]
        public List<MoodleFunction> MoodleFunctions { get; set; }
        [JsonProperty("downloadfiles")]
        public int DownloadFiles { get; set; }
        [JsonProperty("uploadfiles")]
        public int UploadFiles { get; set; }
        [JsonProperty("release")]
        public string Release { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("mobilecssurl")]
        public string MobileCssUrl { get; set; }
        [JsonProperty("advancedfeatures")]
        public List<MoodleAdvancedFeature> MoodleAdvancedFeatures { get; set; }
        [JsonProperty("usercanmanageownfiles")]
        public bool UserCanManageOwnFiles { get; set; }
        [JsonProperty("userquota")]
        public int UserQuota { get; set; }
        [JsonProperty("usermaxuploadfilesize")]
        public int UserMaxUploadFileSize { get; set; }

        #region Error

        [JsonProperty("exception")]
        public string Exception { get; set; }
        [JsonProperty("errorcode")]
        public string ErrorCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        #endregion
    }
}