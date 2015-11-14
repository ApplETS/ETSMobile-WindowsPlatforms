using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    public interface ILoginResult
    {
        bool IsAuthentificated { get; set; }
    }

    public class LoginResult : ILoginResult
    {
        public LoginResult()
        {
            IsAuthentificated = false;
        }

        [JsonProperty("d")]
        public bool IsAuthentificated { get; set; }
    }
}
