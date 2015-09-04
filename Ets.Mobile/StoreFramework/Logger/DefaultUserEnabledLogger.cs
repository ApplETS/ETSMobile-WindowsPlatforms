using System.Diagnostics;
using Splat;

namespace StoreFramework.Logger
{
    public class DefaultUserEnabledLogger : IUserEnabledLogger
    {
        public void Write(string message, LogLevel logLevel)
        {
            Debug.WriteLine(message);
        }

        public LogLevel Level { get; set; }
        public void SetUser(string username)
        {
            Debug.WriteLine($"Username: {username}");
        }
    }
}
