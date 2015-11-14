using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    public class ResultBase
    {
        [JsonProperty("erreur")]
        public string ErrorMessage { get; set; }
    }
}
