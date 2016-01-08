using Newtonsoft.Json;
using System;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Schedule
    {
        [JsonProperty("dateDebut")]
        public DateTime StartDate { get; set; }
        [JsonProperty("dateFin")]
        public DateTime EndDate { get; set; }
        [JsonProperty("coursGroupe")]
        public string CourseAndGroup { get; set; }
        [JsonProperty("nomActivite")]
        public string Name { get; set; }
        [JsonProperty("local")]
        public string Location { get; set; }
        [JsonProperty("descriptionActivite")]
        public string Description { get; set; }
        [JsonProperty("libelleCours")]
        public string Title { get; set; }
    }
}