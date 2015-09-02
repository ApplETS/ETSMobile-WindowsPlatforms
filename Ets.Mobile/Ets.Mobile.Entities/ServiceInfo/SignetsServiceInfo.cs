namespace Ets.Mobile.Entities.ServiceInfo
{
    public interface IClientInfo
    {
        string Url { get; set; }
    }

	public class SignetsServiceInfo : IClientInfo
	{
	    public string Url { get; set; }
	}
}
