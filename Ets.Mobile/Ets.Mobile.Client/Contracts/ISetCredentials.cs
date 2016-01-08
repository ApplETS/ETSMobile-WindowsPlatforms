using Ets.Mobile.Entities.Auth;

namespace Ets.Mobile.Client.Contracts
{
    public interface ISetCredentials
    {
        void SetCredentials(EtsUserCredentials credentials);
    }
}