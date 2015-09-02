using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;
using Newtonsoft.Json;
using Ets.Mobile.Business.Entities.Results.Signets.Interfaces;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    [JsonConverter(typeof(GenericConverter))]
    public class ScheduleAndTeachersResult : ResultBase, IScheduleAndTeachers
    {
        [JsonProperty("listeActivites")]
        public List<Activity> Activities { get; set; }

        [JsonProperty("listeEnseignants")]
        public List<Teacher> Teachers { get; set; }
    }
}
