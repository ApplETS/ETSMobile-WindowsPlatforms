using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Teammate
    {
        [JsonProperty("nom")]
        public string LastName { get; set; }
        [JsonProperty("prenom")]
        public string FirstName { get; set; }
        [JsonProperty("courriel")]
        public string Email { get; set; }
    }
}
