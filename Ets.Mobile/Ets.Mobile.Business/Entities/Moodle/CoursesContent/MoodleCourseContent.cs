using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Moodle.CoursesContent
{
    public class MoodleCourseContent
    {
        public MoodleCourseContent()
        {
            Modules = new List<MoodleCourseModule>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("visible")]
        public bool IsVisible { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("summaryformat")]
        public int SummaryFormat { get; set; }
        [JsonProperty("modules")]
        public List<MoodleCourseModule> Modules { get; set; }
    }
}
