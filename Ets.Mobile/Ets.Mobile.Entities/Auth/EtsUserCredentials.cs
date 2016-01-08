namespace Ets.Mobile.Entities.Auth
{
    public class EtsUserCredentials : UserCredentials
    {
        public EtsUserCredentials()
        {
        }

        public EtsUserCredentials(string username, string password, bool isLoginSuccessful = true) : base(username, password)
        {
            IsLoginSuccessful = isLoginSuccessful;
        }

        public bool IsLoginSuccessful { get; set; }
    }
}