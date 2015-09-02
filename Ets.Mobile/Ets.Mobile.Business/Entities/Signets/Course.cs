using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Course
    {
        [JsonProperty("sigle")]
        public string Acronym { get; set; }

        [JsonProperty("groupe")]
        public string Group { get; set; }

        [JsonProperty("session")]
        public string Semester { get; set; }

        [JsonProperty("programmeEtudes")]
        public string Program { get; set; }

        [JsonProperty("cote")]
        public string Grade { get; set; }

        [JsonProperty("nbCredits")]
        public double Credits { get; set; }

        [JsonProperty("titreCours")]
        public string Name { get; set; }
    }
}
