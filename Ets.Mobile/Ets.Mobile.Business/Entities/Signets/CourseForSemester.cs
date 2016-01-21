using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class CourseForSemester : Activity
    {
        [JsonProperty("listeProf")]
        public Teacher[] Teachers { get; set; }
    }
}