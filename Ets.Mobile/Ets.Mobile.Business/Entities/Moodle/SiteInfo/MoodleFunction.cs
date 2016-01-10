using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Moodle.SiteInfo
{
    public class MoodleFunction
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}