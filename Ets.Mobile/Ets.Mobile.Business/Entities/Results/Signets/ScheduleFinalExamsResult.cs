using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;
using Newtonsoft.Json;
using Ets.Mobile.Business.Entities.Results.Signets.Interfaces;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    [JsonConverter(typeof(GenericConverter))]
    public class ScheduleFinalExamsResult : ResultBase, IScheduleFinalExams
    {
        [JsonProperty("listeHoraire")]
        public List<ScheduleFinalExam> ScheduleFinalExams { get; set; }
    }
}
