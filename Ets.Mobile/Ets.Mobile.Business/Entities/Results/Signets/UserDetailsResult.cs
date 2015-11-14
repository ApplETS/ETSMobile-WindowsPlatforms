using Ets.Mobile.Business.Entities.Results.Signets.Converters;
using Ets.Mobile.Business.Entities.Results.Signets.Interfaces;
using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    [JsonConverter(typeof(GenericConverter))]
    public class UserDetailsResult : ResultBase, IUserDetails
    {
        private string _lastName;
        [JsonProperty("nom")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value?.Trim(); }
        }

        private string _firstName;
        [JsonProperty("prenom")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value?.Trim(); }
        }

        [JsonProperty("codePerm")]
        public string PermanentCode { get; set; }

        [JsonProperty("soldeTotal")]
        public string Balance { get; set; }

        [JsonProperty("masculin")]
        public bool IsMan { get; set; }
    }
}
