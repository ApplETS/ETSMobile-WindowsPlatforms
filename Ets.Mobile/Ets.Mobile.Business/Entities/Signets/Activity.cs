using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Activity
    {
        [JsonProperty("sigle")]
        public string Acronym { get; set; }
        [JsonProperty("groupe")]
        public string Group { get; set; }
        [JsonProperty("jour")]
        public string Day { get; set; }
        [JsonProperty("journee")]
        public string DayName { get; set; }
        [JsonProperty("codeActivite")]
        public string Type { get; set; }
        [JsonProperty("nomActivite")]
        public string Name { get; set; }
        [JsonProperty("activitePrincipale")]
        public string IsPrincipalActivity { get; set; }
        [JsonProperty("heureDebut")]
        public string StartHour { get; set; }
        [JsonProperty("heureFin")]
        public string EndHour { get; set; }
        [JsonProperty("local")]
        public string Location { get; set; }
        [JsonProperty("titreCours")]
        public string Title { get; set; }
    }
}
