using System;
using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class ReplacedDay
    {
        [JsonProperty("dateOrigine")]
        public DateTime OriginDate { get; set; }
        [JsonProperty("dateRemplacement")]
        public DateTime TargetDate { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
