using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Moodle.Courses
{
    public class MoodleCourse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("shortname")]
        public string ShortName { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("enrolledusercount")]
        public int EnrolledUserCount { get; set; }
        [JsonProperty("idnumber")]
        public string IdNumber { get; set; }
        [JsonProperty("visible")]
        public bool IsVisible { get; set; }
        [JsonProperty("Summary")]
        public string Summary { get; set; }
        [JsonProperty("summaryformat")]
        public int SummaryFormat { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("showgrades")]
        public bool ShowGrades { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        [JsonProperty("enablecompletion")]
        public bool EnableCompletion { get; set; }

        #region Exception

        [JsonProperty("exception")]
        public string Exception { get; set; }
        [JsonProperty("errorcode")]
        public string Errorcode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        #endregion
    }
}