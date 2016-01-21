using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Teacher
    {
        [JsonProperty("nom")]
        public string LastName { get; set; }
        [JsonProperty("prenom")]
        public string FirstName { get; set; }
        [JsonProperty("courriel")]
        public string Email { get; set; }
        [JsonProperty("localBureau")]
        public string Location { get; set; }
        [JsonProperty("telephone")]
        public string Phone { get; set; }
        [JsonProperty("enseignantPrincipal")]
        public string IsPrimaryTeacher { get; set; }
    }
}