using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Moodle.CoursesContent
{
    public class MoodleCourseModuleContent
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("filename")]
        public string FileName { get; set; }
        [JsonProperty("filepath")]
        public string FilePath { get; set; }
        [JsonProperty("filesize")]
        public int? FileSize { get; set; }
        [JsonProperty("fileurl")]
        public string FileUrl { get; set; }
        [JsonProperty("timecreated")]
        public object TimeCreated { get; set; }
        [JsonProperty("timemodified")]
        public int? TimeModified { get; set; }
        [JsonProperty("sortorder")]
        public int? SortOrder { get; set; }
        [JsonProperty("userid")]
        public object UserId { get; set; }
        [JsonProperty("author")]
        public object Author { get; set; }
        [JsonProperty("license")]
        public object License { get; set; }
    }
}