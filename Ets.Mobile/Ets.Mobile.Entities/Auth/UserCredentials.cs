using System.Net;

namespace Ets.Mobile.Entities.Auth
{
    public class UserCredentials : ICredentials
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public UserCredentials()
		{
			Username = null;
			Password = null;
		}

		public UserCredentials(string username, string password)
		{
			Username = username;
			Password = password;
		}

        public NetworkCredential GetCredential(System.Uri uri, string authType)
        {
            return new NetworkCredential(Username, Password);
        }
    }
}