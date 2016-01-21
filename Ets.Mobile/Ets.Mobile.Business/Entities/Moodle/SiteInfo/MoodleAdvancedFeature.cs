using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Moodle.SiteInfo
{
    public class MoodleAdvancedFeature
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}