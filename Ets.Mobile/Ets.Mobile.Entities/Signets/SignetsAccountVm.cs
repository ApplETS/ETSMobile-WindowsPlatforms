using Ets.Mobile.Entities.Auth;

namespace Ets.Mobile.Entities.Signets
{
    public class SignetsAccountVm : UserCredentials
    {
        public SignetsAccountVm()
        {
        }

        public SignetsAccountVm(string username, string password, bool isLoginSuccessful = true) : base(username, password)
        {
            IsLoginSuccessful = isLoginSuccessful;
        }

        public bool IsLoginSuccessful { get; set; }
    }
}
