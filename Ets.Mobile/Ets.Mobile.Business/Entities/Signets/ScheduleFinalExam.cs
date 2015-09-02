using System;
using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class ScheduleFinalExam
    {
        [JsonProperty("sigle")]
        public string Abridged { get; set; }
        [JsonProperty("groupe")]
        public string Group { get; set; }
        [JsonProperty("dateExamen")]
        public DateTime Date { get; set; }
        [JsonProperty("heureDebut")]
        public TimeSpan StartHour { get; set; }
        [JsonProperty("heureFin")]
        public TimeSpan EndHour { get; set; }
        [JsonProperty("local")]
        public string Location { get; set; }
    }
}
