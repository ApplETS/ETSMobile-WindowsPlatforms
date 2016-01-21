using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Moodle.Token
{
    public class MoodleToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        #region Error

        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("stacktrace")]
        public string StackTrace { get; set; }
        [JsonProperty("debuginfo")]
        public string DebugInfo { get; set; }
        [JsonProperty("reproductionlink")]
        public string ReproductionLink { get; set; }

        #endregion
    }
}