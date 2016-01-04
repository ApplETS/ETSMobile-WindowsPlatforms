using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Moodle.CoursesContent
{
    public class MoodleCourseModule
    {
        public MoodleCourseModule()
        {
            Contents = new List<MoodleCourseModuleContent>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("instance")]
        public int? Instance { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("visible")]
        public bool? IsVisible { get; set; }
        [JsonProperty("modicon")]
        public string ModIcon { get; set; }
        [JsonProperty("modname")]
        public string ModName { get; set; }
        [JsonProperty("modplural")]
        public string ModPlural { get; set; }
        [JsonProperty("indent")]
        public int? Indent { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("contents")]
        public List<MoodleCourseModuleContent> Contents { get; set; }
    }
}